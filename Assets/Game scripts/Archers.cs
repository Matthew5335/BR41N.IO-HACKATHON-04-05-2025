using UnityEngine;
using System.Collections.Generic;

public class Archer : MonoBehaviour {
    public int damage = 1;
    public int range = 9;

    public void Attack(List<Enemy> enemies) {
        foreach (Enemy e in enemies) {
            if (e.gridIndex >= 16 - range && e.gridIndex < 16) {
                e.TakeDamage(damage);
                Debug.Log($"Archer hit enemy at grid {e.gridIndex} for {damage} damage");
                break; // only 1 shot per turn
            }
        }
    }
}
