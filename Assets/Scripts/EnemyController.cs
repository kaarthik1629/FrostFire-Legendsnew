using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public  int id;
    public bool isDead;

    public static EnemyController instance;
    public  void  Enemygetter(int enemyid)
    {
        if (id == enemyid)
        {
            gameObject.SetActive(true);
        }
        else
        {
            EnemyPooling.instance.ReturnObject(gameObject);
        }
    }
}
