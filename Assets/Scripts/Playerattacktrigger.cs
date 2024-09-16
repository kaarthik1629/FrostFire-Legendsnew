using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playerattacktrigger : MonoBehaviour
{
    public LayerMask Enemylayer ;
    public static Playerattacktrigger instance;
    public Collider playercol;
    public bool isCollided = false;
    public Transform PointRadius;
    public float colliderRadius = 50f;
    private Collider[] enemiesInrange;
    public Color GizmosColor = Color.red;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;    
        }
    }
    public void Start()
    {

        playercol.enabled = false;

    }

    public void Update()
    {
        DetectEnemies();
    }
    private void DetectEnemies()
    {
         enemiesInrange = Physics.OverlapSphere(PointRadius.position, colliderRadius, Enemylayer);
    }
    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Enemy"))
        {
            if(enemiesInrange != null && enemiesInrange.Length > 0)
            {
                foreach (Collider enemy in enemiesInrange)
                {
                    if (enemy == other)
                    {
                        Reducehealth enemyHealth = enemy.GetComponent<Reducehealth>();
                        if (enemyHealth != null)
                        {
                            isCollided = true;

                            enemyHealth.EnemyHealthHit(25);
                            break;
                        }
                        else
                        {
                            Debug.LogError("Reducehealth component not found on enemy: " + enemy.gameObject.name);
                        }

                    }
            
                    
                }



            }
            else
            {
                Debug.LogError("enemiesInRange is null or empty.");
            }




        }
        else
        {
            isCollided = false;
            print("not");
        }

        //Debug.Log("OnTriggerEnter called with: " + enemy.gameObject.name + " on layer: " + enemy.gameObject.tag);

        //if (enemy.transform.gameObject.CompareTag("Enemy"))
        //{
            
        //}
       
    }
    public void EnablePlayerAttackCollider()
    {
        playercol.enabled = true;
    }

    public void DisablePlayerAttackCollider()
    {
        playercol.enabled = false;
    }


    private void OnDrawGizmos()
    {
        if(PointRadius != null)
        {
            Gizmos.color = GizmosColor;


            Gizmos.DrawWireSphere(PointRadius.position, colliderRadius);    


             
        }
    }

}
