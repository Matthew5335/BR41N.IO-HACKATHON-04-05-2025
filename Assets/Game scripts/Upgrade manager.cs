using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class UpgradeManager : MonoBehaviour {
    public Castle castle;
    public GameManager gameManager;

    private bool usedUpgradeThisTurn = false;

    public void UpgradeDamage() {
        if (usedUpgradeThisTurn) return;

        foreach (Archer a in castle.archers) {
            a.damage += 1;
        }

        Debug.Log("Upgraded Archer Damage");
        usedUpgradeThisTurn = true;
    }

    public void UpgradeRange() {
        if (usedUpgradeThisTurn) return;

        foreach (Archer a in castle.archers) {
            a.range += 1;
        }

        Debug.Log("Upgraded Archer Range");
        usedUpgradeThisTurn = true;
    }

    public void UpgradeCastleHP() {
        if (usedUpgradeThisTurn) return;
        castle.hp += 25;
        castle.maxHP += 25;
        castle.UpdateHealthDisplay();
        Debug.Log("Increased Castle HP");
        usedUpgradeThisTurn = true;
    }

    public void AddArcher() {
        if (usedUpgradeThisTurn) return;

        if (castle.AddNewArcher()) {
            Debug.Log("Added new archer.");
            usedUpgradeThisTurn = true;
        }
    }
    


    public int freezeCooldownTurns = 3;

    public int zone1Cooldown = 0;
    public int zone2Cooldown = 0;
    public int zone3Cooldown = 0;

    public TextMeshProUGUI zone1Text, zone2Text, zone3Text;
    public void TickCooldowns() {
        if (zone1Cooldown > 0) zone1Cooldown--;
        if (zone2Cooldown > 0) zone2Cooldown--;
        if (zone3Cooldown > 0) zone3Cooldown--;

        UpdateCooldownLabels();
    }

    void UpdateCooldownLabels() {
        zone1Text.text = zone1Cooldown > 0 ? $"Cooldown: {zone1Cooldown}" : "READY";
        zone2Text.text = zone2Cooldown > 0 ? $"Cooldown: {zone2Cooldown}" : "READY";
        zone3Text.text = zone3Cooldown > 0 ? $"Cooldown: {zone3Cooldown}" : "READY";
    }


    public void FreezeZone1() {
        if (usedUpgradeThisTurn || zone1Cooldown > 0) return;

        FreezeEnemies(0, 3);
        zone1Cooldown = freezeCooldownTurns;
        usedUpgradeThisTurn = true;
        UpdateCooldownLabels();
    }

    public void FreezeZone2() {
        if (usedUpgradeThisTurn || zone2Cooldown > 0) return;

        FreezeEnemies(4, 7);
        zone2Cooldown = freezeCooldownTurns;
        usedUpgradeThisTurn = true;
        UpdateCooldownLabels();
    }

    public void FreezeZone3() {
        if (usedUpgradeThisTurn || zone3Cooldown > 0) return;

        FreezeEnemies(8, 11);
        zone3Cooldown = freezeCooldownTurns;
        usedUpgradeThisTurn = true;
        UpdateCooldownLabels();
    }

    void FreezeEnemies(int start, int end) {
        foreach (var e in gameManager.enemies) {
            if (e.gridIndex >= start && e.gridIndex <= end) {
                e.frozen = true;
                e.UpdateFrozenVisual();
            }
        }
    }




    public void ResetTurnFlag() {
        usedUpgradeThisTurn = false;
    }
}

