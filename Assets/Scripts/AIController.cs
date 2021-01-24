using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour{
    public NavMeshAgent npc;
    public FieldOfView fov;
    public Ranking ranking;
    public Transform transformDestination;
    public bool isDead;
    private Vector3 _tempTransform;
    private bool _isArrived;

    void Start(){
        npc = GetComponent<NavMeshAgent>();
        fov = GetComponent<FieldOfView>();
        transformDestination = GameObject.FindWithTag("Finish").transform;
        ranking = transform.parent.GetComponent<Ranking>();
        _tempTransform = transformDestination.position;
    }

    void FixedUpdate(){
        if (!isDead){
            DestinationChanger();
        }

        if (isDead){
            npc.updatePosition = false;
            npc.updateRotation = false;
        }
    }

    //önüne engel geldiğinde destination değiştirip engelleri geçmesini sağlıcaz
    private void DestinationChanger(){
        if (fov.visibleTargets.Count > 0 && !_isArrived){
            if (fov.visibleTargets != null){
                var a = fov.visibleTargets[0].GetComponent<AIObstaclesDestination>().destination;
                if (a.Length == 1){
                    _tempTransform = a[0].position;
                    npc.destination = _tempTransform;
                    _isArrived = true;
                }else if (a.Length == 2){
                    _tempTransform = a[Random.Range(0, 2)].position;
                    npc.destination = _tempTransform;
                    _isArrived = true;
                }
            }
        }
        Debug.Log(_tempTransform);
        DestinationArriveCheck(_tempTransform);
    }

    private void DestinationArriveCheck(Vector3 t){
        if (Mathf.Abs(npc.transform.position.x - t.x) <= 0.1f  
            && Mathf.Abs(npc.transform.position.z - t.z) <= 0.1f ){
            _tempTransform = transformDestination.position;
            _isArrived = false;
        }
        
        npc.destination = _tempTransform;
    }

    public void SpawnAtStart(){
        SpawnLo sl = transform.parent.GetComponent<SpawnLo>();
        GameObject temp = Instantiate(sl.npcPrefab, sl.spawnLocations[Random.Range(0, 5)].position, Quaternion.identity, sl.transform);
        ranking.racers.Remove(transform);
        ranking.racers.Add(temp.transform);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.CompareTag("Finish")){
            if (!ranking.last.Contains(transform)){
                ranking.last.Add(transform);
            }
            ranking.isGameFinished = true;
            Destroy(gameObject);
        }
    }
}