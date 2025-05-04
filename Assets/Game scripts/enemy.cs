using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour {
    public int maxHP = 4;
    public int hp = 4;
    private TextMeshPro textMesh;
    private SpriteRenderer spriteRenderer;

    public int gridIndex = 0;
    public bool frozen = false;

    private Transform[] gridSlots;

    void Start() {
        gridSlots = GameObject.Find("GridParent").GetComponent<GridManager>().gridSlots;
        transform.position = gridSlots[gridIndex].position;

        GameObject textObj = new GameObject("HP Text");
        textObj.transform.SetParent(transform);
        textObj.transform.localPosition = new Vector3(0, 2.2f, 0);

        textMesh = textObj.AddComponent<TextMeshPro>();
        textMesh.fontSize = 2.5f;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.color = Color.red;
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateFrozenVisual();

        UpdateHealthDisplay();
    }


    public void Move() {
        if (gridIndex < gridSlots.Length - 1) {
            gridIndex++;
            transform.position = gridSlots[gridIndex].position;
        }
    }

    public void TakeDamage(int dmg) {
        hp -= dmg;
        UpdateHealthDisplay();

        if (hp <= 0) {
            Destroy(gameObject);
            GameManager.Instance.enemies.Remove(this);
        }
    }

    void UpdateHealthDisplay() {
        if (textMesh != null) {
                textMesh.text = $"{EnemySpawner.enemiesPerSpawn}x: {hp}/{maxHP}";
        }
    }


    public void UpdateFrozenVisual() {
        if (spriteRenderer != null)
            spriteRenderer.color = frozen ? Color.cyan : Color.white;
    }


}

