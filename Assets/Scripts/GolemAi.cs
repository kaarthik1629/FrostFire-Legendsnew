using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class GolemAi : MonoBehaviour
{
   [SerializeField] public Transform player;
    public Transform[] PatrolPoints;
    public float ChaseRange = 15f;
    public float AttackRange = 2f;
    public float PatrolSpeed = 2f;
    public float ChaseSpeed = 5f;
    public float AttackCoolDown = 1f;
    private NavMeshAgent agent;
    private Animator anigolem;
    public static GolemAi instance;
    public int CurrentPatrolIndex;
    private bool ischasing;
    private bool isattacking;
    public float NextAttackTime;
    public float stopDistanceAfterAttack = 1f;
    public float distanceThreshold = 3f;
    public AudioClip[] footstepClips;
    public AudioSource audioSource;
    public AudioClip WalkingSound;
   // public AudioClip rhinoChase;
    
    //public bool isCollided = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
       
        
    }
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anigolem = GetComponent<Animator>();
        CurrentPatrolIndex = 0;
        Patrol();

        if (audioSource == null)
        {

          audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {
       

    }

    private void FixedUpdate()
    {
        
        if (!gameObject.activeInHierarchy) 
        {
            return;
        }

        else
        {
            float distanceToPlayer = Vector3.Distance(player.position, transform.position);
            if (isattacking)
            {
                FacePlayer();
                agent.destination = player.transform.position;
                return;
            }

            if (distanceToPlayer <= AttackRange)
            {
                Attack();
            }
            else if (distanceToPlayer <= ChaseRange)
            {
                Chase();

                
            }
            else if (!agent.pathPending && agent.remainingDistance < 0.5f )
            { 
              Patrol();
               // audioSource.PlayOneShot(rhinoChase);
            }
            else
            {
                if (ischasing)
                {
                    ischasing = false;
                    anigolem.SetBool("isRunning", false);
                    anigolem.SetBool("isWalking", true);
                    agent.speed = PatrolSpeed;
                    Patrol();

                }



            }

        }

        
        

    }
    void Patrol()
    {
        if (ischasing) return;

        anigolem.SetBool("isWalking", true);
        anigolem.SetBool("isRunning", false);
        agent.speed = PatrolSpeed;
        if (!gameObject.activeInHierarchy) return;


        if (agent.remainingDistance < 0.5f && !agent.pathPending )
        {
            CurrentPatrolIndex = (CurrentPatrolIndex + 1) % PatrolPoints.Length;
            agent.destination = PatrolPoints[CurrentPatrolIndex].position;
        }
        else
        {
            return;
        }

       
    }
    void Chase()
    {
        if (isattacking) return;
       
        ischasing = true;
        anigolem.SetBool("isWalking", false);
        anigolem.SetBool("isRunning", true);
        agent.speed = ChaseSpeed;
        agent.destination = player.position;
        


    }
    void Attack()
    {
        if (Time.time < NextAttackTime) return;

        isattacking = true;
        agent.isStopped = true;

        anigolem.SetBool("isRunning", false);
        anigolem.SetBool("isWalking",false);
        anigolem.SetTrigger("attack");

        NextAttackTime = Time.time + AttackCoolDown;
        Invoke("ResetAttack", AttackCoolDown);

    }
    
 


    private void ResetAttack()
    {
        isattacking = false;

        if (!gameObject.activeInHierarchy) return;

        float distanceToPlayer = Vector3.Distance(player.position,transform.position);
        if (distanceToPlayer <= ChaseRange)
        {
            if(distanceToPlayer > AttackRange + distanceThreshold)
            {
                
                Chase();
            }
            else
            {
                anigolem.SetBool("isRunning", false);
                anigolem.SetBool("isWalking", false);
                agent.SetDestination(transform.position);   
                   
            }
           
        }
        else 
        {
           
            Patrol();
        }
        ischasing = true;
        agent.isStopped = false;
        
    }

    void FacePlayer()
    {
        Vector3 direction =(player.position - transform.position).normalized;   
        Quaternion lookroatation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookroatation, Time.deltaTime * 5);
    }
    private void OnAnimatorMove()
    {
        if (isattacking)
        {
            agent.velocity = Vector3.zero;  
            agent.destination = transform.position;
            Invoke("ResumeMovement",stopDistanceAfterAttack);
        }
    }
    private void ResumeMovement()

    {

        if (!gameObject.activeInHierarchy) return;

        if (ischasing)
        {
            if (!gameObject.activeInHierarchy) return;

            agent.speed = ChaseSpeed;
            agent.destination = player.position;

        }
        else
        {
          
            Patrol();   

        }

         
    }
    public void EnemyAttackDetect()
    {
        if (EnemyAttack.Instance != null)
        {
            EnemyAttack.Instance.enabled = true;
        }
        
    }
    public void golemAttackSound()
    {
        AudioManager.instance.PlaySfx("GolemAttack");
    }
    public void RhinoAttackSound()
    {
        AudioManager.instance.PlaySfx("RhinoAttack");
    }
    public void golemChaseSound()
    {
        AudioManager.instance.PlaySfx("GolemChase");
    }
    public void RhinoChaseSound()
    {
        AudioManager.instance.PlaySfx("RhinoChase");
    }
    public void GolemDeathsound()
    {
        AudioManager.instance.PlaySfx("GolemDeath");
    }
    public void RhinoDeath()
    {
        AudioManager.instance.PlaySfx("RhinoDeath");
    }
    public void Golemhitsound()
    {
        AudioManager.instance.PlaySfx("GolemHit");
    } 
    public void Rhinohitsound()
    {
        AudioManager.instance.PlaySfx("RhinoHit");
       // AudioManager.instance.PlaySfx("FireSword");
    }
    public void PlayRandomFootSteps()
    {
        if (footstepClips.Length == 0) return;

        int randomIndex = Random.Range(0, footstepClips.Length);
        AudioClip randomClip = footstepClips[randomIndex];

        audioSource.PlayOneShot(randomClip);
        if (ischasing)
        {
            CinemachineShake.Instance.ShakeCamera(1f, .1f);

        }

        



    }
    public void PlayWalkingGolem()
    {
        audioSource.PlayOneShot(WalkingSound);
    } 
    public void PlayWalkingRhino()
    {
        audioSource.PlayOneShot(WalkingSound);
        if(ischasing)
        {
            CinemachineShake.Instance.ShakeCamera(2f, .1f);

        }
    
    }




    
    

}




