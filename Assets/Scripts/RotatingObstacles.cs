using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingObstacles : MonoBehaviour{
    public float speed;
    public float impactSpeed;
    public Vector3 rotateLoc;
    [Range(-1, 1)]
    public int rotateDirection;
    
    private void Update(){
        RotateObstacle(rotateLoc);
    }
    
    //Rotate kontrol eden method
    private void RotateObstacle(Vector3 a){
        transform.Rotate(a * (rotateDirection * speed * Time.deltaTime));
    }
    private void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("NPC")){
            Debug.Log("a");
        }
    }

}