using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Animator Ani;
    private float NextAttacktime = 0f;
    private bool isAttacking = false;
    public float attackCoolDown = 1f;
    public GameObject Lightfx;
    public GameObject secondlightfx;
    public GameObject Heavyfx;

    
    
    // Start is called before the first frame update
    void Start()
    {
         Ani = GetComponent<Animator>();    
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.timeScale == 0f) return;
       // if (PlayerHealth.playerDead == true) return;

        if (Time.time >= NextAttacktime) 
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                LightAttack();
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                HeavyAttack();
            }
        }
    }
    void LightAttack()
    {
        if (PlayerHealth.playerDead == true) return;
        if (isAttacking) return;
        isAttacking = true;
        int randomAttacktype = Random.Range(0, 2);


        if (randomAttacktype == 0 ) 
        {
            Ani.SetTrigger("LightAttack1");
            AudioManager.instance.PlaySfx("FireSword");
            //LightAttackFx();

        }
        else
        {
            Ani.SetTrigger("LightAttack2");
            AudioManager.instance.PlaySfx("FireSword");
            //SecondLightAttackFx();
        }
       // Ani.SetTrigger("LightAttack1");
        NextAttacktime = Time.time + attackCoolDown;
        Invoke("ResetAttack", attackCoolDown);

    }
 
    void HeavyAttack()
    {
        if (PlayerHealth.playerDead == true) return;
        if (isAttacking) return;
        isAttacking = true;
        AudioManager.instance.PlaySfx("FireHeavy");
        Ani.SetTrigger("HeavyAttack");
        NextAttacktime = Time.time + attackCoolDown;
        Invoke("ResetAttack", attackCoolDown);

    }
    void ResetAttack()
    {
        if (PlayerHealth.playerDead == true) return;
        isAttacking = false;
    }
    public void LightAttackFx()
    {
        Lightfx.SetActive(true);
       

    }
    public void DisableLightFx()
    {
        Lightfx.SetActive(false);
    }
    public void SecondLightAttackFx()
    {
        secondlightfx.SetActive(true);

    }
    public void DisableSecondLightFx()
    {
        secondlightfx.SetActive(false);
    }
    public void HeavyFx()
    {
        Heavyfx.SetActive(true);

    }
    public void DisableHeavyFx()
    {
        Heavyfx.SetActive(false);
    }



    
}

