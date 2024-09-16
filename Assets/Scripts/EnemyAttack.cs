using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    public Collider enemycol;
    public bool isCollided = false;
    public static EnemyAttack Instance;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
    }
    public void Start()
    {

        enemycol.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            print("attacked");
            isCollided = true;
            PlayerHealth.instance.Health(25);
            CinemachineShake.Instance.ShakeCamera(5f, .2f);
            print(PlayerHealth.instance.CurrentHealth);
            
            

        }
        else 
        {
            isCollided = false;
           //this.enabled = false; 
            
        }
       
        
       
        
    }
    public void EnableAttackCollider()
    {
        enemycol.enabled = true;
    }

    public void DisableAttackCollider()
    {
        enemycol.enabled = false;
    }



}
