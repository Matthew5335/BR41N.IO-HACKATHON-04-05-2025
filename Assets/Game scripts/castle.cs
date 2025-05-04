using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Castle : MonoBehaviour {
    public int hp = 100;
    public int maxHP = 100;
    private TextMeshPro textMesh;


    void Start() {
        GameObject textObj = new GameObject("Castle HP Text");
        textObj.transform.SetParent(transform);
        textObj.transform.localPosition = new Vector3(0, 2f, 0); // Adjust position

        textMesh = textObj.AddComponent<TextMeshPro>();
        textMesh.fontSize = 3;
        textMesh.alignment = TextAlignmentOptions.Center;
        textMesh.color = Color.magenta;

        UpdateHealthDisplay();
    }


    public GameObject archerPrefab;
    public Transform[] archerSlots;
    public List<Archer> archers = new List<Archer>();


    public void UpdateHealthDisplay() {
        if (textMesh != null)
            textMesh.text = hp + "/" + maxHP;
    }

    public void TakeDamage(int amount) {
        hp -= amount;
        UpdateHealthDisplay();

        if (hp <= 0) {
            Debug.Log("Castle destroyed!");
        }
    }

    public bool AddNewArcher() {
        for (int i = 0; i < archerSlots.Length; i++) {
            bool slotTaken = archers.Exists(a => a.transform.parent == archerSlots[i]);

            if (!slotTaken) {
                GameObject newArcher = Instantiate(archerPrefab, archerSlots[i].position, Quaternion.identity);
                newArcher.transform.parent = archerSlots[i];
                Archer archer = newArcher.GetComponent<Archer>();
                archers.Add(archer);
                return true;
            }
        }

        Debug.Log("No empty archer slots available.");
        return false;
    }


}
