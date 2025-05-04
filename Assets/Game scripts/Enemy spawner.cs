using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour {
    public GameObject enemyPrefab;
    public float spawnInterval = 9f; 
    public int baseHP = 4;            
    public static int enemiesPerSpawn = 1;
    private bool stopSpawning = false;


    private int turnCount = 0;

    void Start() {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    public void SpawnEnemy() {
        if (stopSpawning) return;

        turnCount++;

        // Optional scaling:
        if (turnCount % 12 == 0) enemiesPerSpawn++;  
        if (turnCount % 3 == 0) baseHP++;           

        for (int i = 0; i < enemiesPerSpawn; i++) {
            GameObject obj = Instantiate(enemyPrefab);
            Enemy e = obj.GetComponent<Enemy>();

            e.hp = baseHP;
            e.maxHP = baseHP; 

            GameManager.Instance.enemies.Add(e);
        }

        Debug.Log($"[Wave {turnCount}] Spawned {enemiesPerSpawn} enemy(ies) with {baseHP} HP");
    }

    void StopSpawning() {
        stopSpawning = true;
    }


}
