using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PersistentStatsController{
    [Header("Player Stats")]
    public int playerLivesLost;
    public int playerShotsFired;
    public int atPoints;
    public float playerStartingHP;

    [Header("Player Abilities")]
    public int playerHPAP;
    public int playerSSAP;
    public int playerRegenAP;
    public int spawnAddAP;

    [Header("Gun Stats")]
    public float gunStartingPS;
    public float gunStartingFR;
    public float gunStartingPD;

    [Header("Gun Abilities")]
    public int gunFRAP;
    public int gunPSAP;
    public int gunPDAP;

    public bool spreadUnlocked;
    public int spreadFRAP;
    public int spreadPSAP;
    public int spreadPDAP;

    [Header("Ability Point Stats")]
    public int spThisRound;
    public int spendablePoints;
    public float spFactor;
    public int spFactorAP;

    [Header("Combo Stats")]
    public int highestCombo;
    public float longestCombo;
    public int currentCombo;
    public float currentComboTimer;
    public int comboTimerMaxAP;

    [Header("Stage Stats")]
    public int highestStage;
    public int startingStage;

    [Header("Enemy Stats")]
    public int enemiesDefeated;

    [Header("Shop Stats")]
    public int totalPurchases;
    public int cost_playerHPAP;
    public int cost_playerRegenAP;
    public int cost_comboTimerMaxAP;
    public int cost_spFactorAP;
    public int cost_gunFRAP;
    public int cost_gunPSAP;
    public int cost_gunPDAP;
    public int cost_spreadUnlock;
    public int cost_spreadFRAP;
    public int cost_spreadPSAP;
    public int cost_spreadPDAP;
    public int cost_playerSSAP;
    public int cost_spawnAddAP;

    [Header("Player Current")]
    public float currentPlayerHP;

    [Header("Gun Current")]
    public float currentGunPS;
    public float currentGunFR;
    public float currentGunPD;

    [Header("Game Current")]
    public int currentPoints;
    public int currentStage;

    [Header("Options Configuration")]
    public float masterVolume;
    public float sfxVolumeSet;
    public float bgmVolumeSet;

}
