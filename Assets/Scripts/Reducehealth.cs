using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;
using DG.Tweening;

public class Reducehealth : MonoBehaviour
{
    public Slider enemyslider;
    public Slider Easehealthslider;
   // public GameObject enemyobj;
    public float CurrentHealth;
    private float LerpSpeed = 0.05f;
    public float maxhealth = 100f;
    private Animator animator;
    public static bool IssDeadEnemy;
    public GameObject Hp;
    public GameObject HpCollect;
    public Transform HpSpawnPoint;

   

    public static Reducehealth instance;
    
    // Start is called before the first frame update


    void Start()
    {

        animator = GetComponent<Animator>();


        //if (instance == null)
        //{
        //    instance = this;
        //}


        InitializeHealth();
    }

    public void InitializeHealth()
    {
        CurrentHealth = maxhealth;
        if (enemyslider != null && Easehealthslider != null)
        {
            enemyslider.maxValue = maxhealth;
            enemyslider.value = CurrentHealth;
            Easehealthslider.maxValue = maxhealth;
            Easehealthslider.value = CurrentHealth;
            IssDeadEnemy = false;
        }
    }
    private void Update()
    {


        if (Easehealthslider.value != enemyslider.value)
        {
            Easehealthslider.value = Mathf.Lerp(Easehealthslider.value, enemyslider.value, LerpSpeed);
        }
    }




    public void EnemyHealthHit(float damage)
    {

        if (CurrentHealth <= 0) return;

        CurrentHealth -= damage;
        CinemachineShake.Instance.ShakeCamera(3f, .1f);
        print(damage);
        enemyslider.value = CurrentHealth;
        // Instantiate(Hp,transform.position, Quaternion.identity);
        GameObject VfxHp = Instantiate(Hp, HpSpawnPoint.position, Quaternion.identity);

        Vector3 targetPosition = HpSpawnPoint.position + new Vector3(Random.Range(-4f, 2f),1f,Random.Range(-4f,2f));

        VfxHp.transform.DOJump(targetPosition, 2f, 1, 1f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                VfxHp.transform.DOMoveY(HpSpawnPoint.position.y - 1f, 1f)
                .SetEase(Ease.InOutBounce)
                 .OnComplete(() =>
                 {
                    GameObject collectedobj = Instantiate(HpCollect, VfxHp.transform.position, Quaternion.identity);
                     Destroy(VfxHp);
                     Destroy(collectedobj, 10f);


                 });
            });


       




        if (!gameObject.activeInHierarchy) return;
        if (enemyslider != null)
        {
            if (CurrentHealth > 0)
            {
                animator.SetTrigger("GetHit");
            }
            enemyslider.value = CurrentHealth;

            if (CurrentHealth <= 0)
            {
                // if (!gameObject.activeInHierarchy) return;
                StartCoroutine(HandleDeath());

            }
        }
    }
    IEnumerator HandleDeath()
    {
        if (!gameObject.activeInHierarchy) yield break;

        animator.SetTrigger("Die");

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        float animationLength = stateInfo.length;
        Debug.Log("Death animation length :" + animationLength);
        yield return new WaitForSeconds(animationLength);

        Debug.Log("Deactivating enemy: " + gameObject.name);

        IssDeadEnemy = true;

        EnemyPooling.instance.ReturnObject(gameObject);
    }
    private void OnDisable()
    {
        InitializeHealth(); 
    }


}
