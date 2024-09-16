using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpHeal : MonoBehaviour
{
   // public int healAmount = 10; 
    public ParticleSystem particleSystem;
   // private float destroyDelay = 10f;
    private bool isCollected = false;
    // Start is called before the first frame update

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
       
    }
    void Start()
    {
        var collision = particleSystem.collision;
        collision.sendCollisionMessages = true;
        
       

    }
   
    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player")&& !isCollected)
        {
            isCollected = true;
            print("collided");
            PlayerHealth.instance.Heal(10);
            Destroy(gameObject);


          


        }
        //else if(isCollected) 
        //{
        //    Invoke("DestroyCollectible", destroyDelay);
        //}
       
    }
    //void DestroyCollectible()
    //{
    //    Destroy(gameObject);
    //}



}
