using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Slider EnemyHealthSlider;
    public Slider EnemyEaseHealthSlider;
    private float LerpSpeed = 0.05f;
    public static EnemyHealth instance;
    public float CurrentHealth;
    public Animator animator;
    public GameObject enemyobj;
    public static bool IsEnemyDead;
    // private Transform enemyparent;

    // Start is called before the first frame update
    private void Awake()
    {
        IsEnemyDead = false;

        if (instance == null)
        {
            instance = this;
        }


        animator = GetComponent<Animator>();


    }
    //private void OnEnable()
    //{




    //}
    void Start()
    {
        // Slider health = GetComponent<Slider>();
        // Slider Easehealth = GetComponent<Slider>();



    }
    void Update()
    {
        if (EnemyEaseHealthSlider.value != EnemyHealthSlider.value)
        {
            EnemyEaseHealthSlider.value = Mathf.Lerp(EnemyEaseHealthSlider.value, EnemyHealthSlider.value, LerpSpeed);
        }
    }
    //public void EnemyHealthHit(int damage)
    //{

    //    if(CurrentHealth <= 0) return;

    //    CurrentHealth -= damage;
    //    print(maxHealth);
    //    EnemyHealthSlider.value = CurrentHealth;

    //    if (!gameObject.activeInHierarchy) return;
    //    if (EnemyHealthSlider != null)
    //    {
    //        if (CurrentHealth > 0)
    //        {
    //            animator.SetTrigger("GetHit");
    //        }
    //        EnemyHealthSlider.value = CurrentHealth;
    //    }

    //    if (CurrentHealth <= 0)
    //    {
    //        // if (!gameObject.activeInHierarchy) return;
    //       // Destroy(EnemyHealthSlider.gameObject);
    //       // Destroy(EnemyEaseHealthSlider.gameObject);


    //        StartCoroutine(HandleDeath());

    //    }
    //}
    //IEnumerator HandleDeath()
    //{
    //    if(!gameObject.activeInHierarchy) yield break; 

    //    animator.SetTrigger("Die");

    //    AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

    //    float animationLength = stateInfo.length;
    //    Debug.Log("Death animation length :" + animationLength);
    //    yield return new WaitForSeconds(animationLength+ 0.1f);

    //    Debug.Log("Deactivating enemy: " + gameObject.name);

    //    //GolemAi.instance.enabled = false;
    //    //EnemyHealth.instance.enabled = false;

    //    transform.GetComponent<GolemAi>().enabled = false;
    //    Destroy(enemyobj);

    //   // GolemManager.instance.OnGolamKilled();

    //    //if(enemyobj != null)
    //    //{

    //    //    EnemyPool.instance.ReturnEnemyToPool(gameObject);
    //    //}
    //}
    ////public void InitializeEaseHealth(Slider EnemyEaseHealthSlider)
    ////{

    ////    CurrentHealth = maxHealth;

    ////    EnemyEaseHealthSlider.maxValue = maxHealth;
    ////    EnemyEaseHealthSlider.value = CurrentHealth;

    ////}
    ////public void InitializeHealth(Slider EnemyHealthSlider)
    ////{
    ////    CurrentHealth = maxHealth;

    ////    EnemyHealthSlider.maxValue = maxHealth;
    ////    EnemyHealthSlider.value = CurrentHealth;
    ////}






    ////private void OnEnable()
    ////{
    ////    InitializeHealth();
    ////}
    ////public void AssignSliders(Slider healthSlider, Slider EasehealthSlider)
    ////{
    ////    EnemyHealthSlider = healthSlider;
    ////    EnemyEaseHealthSlider = EasehealthSlider;
    ////    InitializeHealth();

    ////}





}
