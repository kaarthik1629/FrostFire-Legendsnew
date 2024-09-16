using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPooling : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    private Queue<GameObject> inactiveEnemies = new Queue<GameObject>();
    public static EnemyPooling instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        foreach (GameObject enemy in enemyPrefabs)
        {
            enemy.SetActive(false);
            inactiveEnemies.Enqueue(enemy);
        }
        Debug.Log("Enemy pool initialized with " + inactiveEnemies.Count + " enemies.");
    }
    private void Update()
    {
        GetObject();
        
        //for (int i = 0; i >= enemyPrefabs.Length; i++)
        //{
        //    EnemyController enemy = GetComponent<EnemyController>();
        //    if (enemyPrefabs.Length < 0)
        //    {
        //        enemy.Enemygetter(i);
        //    }
        //    else
        //    {
        //        i--;
        //Debug.Log(i);
        //    }
        //}
    }

    public GameObject GetObject()
    {
       
        if(inactiveEnemies.Count > 0) 
        {
            
            GameObject enemy = inactiveEnemies.Dequeue();

            return enemy;
        }
        else
        {
            Debug.LogWarning("No inactive enemies available.");
            return null; // or handle as needed if you need to spawn a new one
        }
    }

    public void ReturnObject(GameObject enemy)
    {
        
        enemy.SetActive(false);
        
        inactiveEnemies.Enqueue(enemy);
    }



}