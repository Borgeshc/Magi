using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyType;
    public Transform[] spawnPositions;

    public Text killCounter;

    List<GameObject> enemies = new List<GameObject>();

    int wavecount;
    int enemiesRequired = 0;
    int killCount;

    bool waveInProgress;
    
	void Start ()
    {
		
	}
	
	void Update ()
    {
		if(!waveInProgress)
        {
            waveInProgress = true;
            wavecount++;
            enemiesRequired += 2;

            for(int i = 0; i < enemiesRequired; i++)
            {
                GameObject newEnemy = Instantiate(enemyType, spawnPositions[Random.Range(0, spawnPositions.Length)].position, Quaternion.identity);
                enemies.Add(newEnemy);
            }
        }

        if(enemies.Count <= 0)
        {
            waveInProgress = false;
        }
	}

    public void RemoveEnemy(GameObject enemy)
    {
        killCount++;
        killCounter.text = "Kill Count: " + killCount;
        enemies.Remove(enemy);
    }
}
