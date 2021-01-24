using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Obstacles : MonoBehaviour{
    public float impactSpeed;//çarptığında olacak etkiyi ayarlayan değişken

    public void OnCollisionEnter(Collision other){
        if (other.gameObject.CompareTag("Player")){
            var a = other.gameObject.GetComponent<PlayerController>();
            a.cantMove = true;
            a.isPlayerDead = true;
            a.animatorPlayer.SetBool("isDead", true);
            ImpactorBetweenPlayerAndObs(other.transform);
        }
        
        if (other.gameObject.CompareTag("NPC")){
            other.gameObject.GetComponent<AIController>().isDead = true;
            ImpactorBetweenPlayerAndObs(other.transform);
            other.gameObject.GetComponent<AIController>().SpawnAtStart();
        }
    }
    
    //objeye çarptığında kuvvet uyguluyoruz.
    public virtual void ImpactorBetweenPlayerAndObs(Transform player){
        player.GetComponent<Rigidbody>().AddForce((GetAngles(player) * impactSpeed), ForceMode.Impulse);
    }

    //obstacle çarptığında hangi yöne doğru kuvvet uygulanacağını hesaplıyor.
    public Vector3 GetAngles(Transform player){
        return (player.position - transform.position);
    }
    
}
