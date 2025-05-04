using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public UpgradeManager upgradeManager;

    public float turnDuration = 5f;
    private float turnTimer;

    public List<Enemy> enemies = new List<Enemy>();
    public Castle castle;
    public GameObject mainUI; 


    public GameObject gameOverScreen;
    private bool gameOver = false;


    private void Awake() {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start() {
        StartCoroutine(TurnLoop());
    }

    IEnumerator TurnLoop() {
        while (castle.hp > 0) {
            Debug.Log("New Turn");

            foreach (Archer archer in castle.archers) {
                archer.Attack(enemies);
            }

            for (int i = enemies.Count - 1; i >= 0; i--) {
                Enemy enemy = enemies[i];

                if (!enemy.frozen) {
                    enemy.Move();
                }

                if (enemy.gridIndex >= 12) {
                    castle.TakeDamage(10);
                }
            }

            foreach (Enemy enemy in enemies) {
                enemy.frozen = false;
                enemy.UpdateFrozenVisual();
            }

            if (castle.hp <= 0) {
                TriggerGameOver();
                yield break;
            }

            if (upgradeManager != null) {
                upgradeManager.TickCooldowns();
                upgradeManager.ResetTurnFlag();
            } else {
                Debug.LogWarning("UpgradeManager is not assigned in GameManager!");
            }

            yield return new WaitForSeconds(turnDuration);
        }

        Debug.Log("Game Over!");
    }

    void TriggerGameOver() {
        gameOver = true;
        Debug.Log("Game Over!");

        if (mainUI != null)
            mainUI.SetActive(false);

        if (gameOverScreen != null)
            gameOverScreen.SetActive(true);
    }



}
