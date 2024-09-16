using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public Slider PlayerHealthSlider;
    public Slider EaseHealthSlider;
    private float LerpSpeed = 0.05f;
    public static PlayerHealth instance;
    public float CurrentHealth;
    public Animator ani;
    public GameObject Deathmenu;
    public static bool playerDead ;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        playerDead = false;
    }
    void Start()
    {

       
        CurrentHealth = maxHealth;
        PlayerHealthSlider.maxValue = maxHealth;                    
        PlayerHealthSlider.value = CurrentHealth;
        EaseHealthSlider.maxValue = maxHealth;
        EaseHealthSlider.value = CurrentHealth;
    }

    void Update()
    {
        if (EaseHealthSlider.value != PlayerHealthSlider.value)
        {
            EaseHealthSlider.value = Mathf.Lerp(EaseHealthSlider.value, PlayerHealthSlider.value, LerpSpeed );
        }
    }

    public void Health (int damage)
    {
        CurrentHealth -= damage;
           
        PlayerHealthSlider.value = CurrentHealth;
        if(CurrentHealth > 0) 
        {
           ani.SetTrigger("GetHit");

        }
        else if(CurrentHealth <= 0) 
        {
            playerDead = true;
            ani.SetTrigger("Death");

            
        }
        
    }

    //IEnumerator PlayerDeath()
    //{
    //    ani.SetTrigger("Death");
    //    AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);

    //    float animationLength = stateInfo.length;
    //    Debug.Log("Death animation length :" + animationLength);
    //    yield return new WaitForSeconds(animationLength + 0.01f);

    //    playerDead = true;



    //}

    public void Heal(int heal)
    {
        if (CurrentHealth >= maxHealth) return;
        CurrentHealth += heal;
        PlayerHealthSlider.value = CurrentHealth;

    }

    public void Death()
    {
        MainMenu.instance.PlayerDeath();
       
    }
}
