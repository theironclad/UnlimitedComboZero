using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PersistentStatsController{

    [Header("Player")]
    [Header("Player Stats")]
    public int playerLivesLost;
    public int playerShotsFired;
    public int atPoints;
    public float playerStartingHP;

    [Header("Abilities")]
    [Header("Player Abilities")]
    public int playerHPAP;
    public int playerSSAP;
    public int playerRegenAP;
    public int spawnAddAP;
    public bool shieldUnlocked;
    public int playerShieldAP;
    public int comboTimerMaxAP;

    [Header("Gun Abilities")]
    public int gunFRAP;
    public int gunPSAP;
    public int gunPDAP;

    public bool spreadUnlocked;
    public int spreadFRAP;
    public int spreadPSAP;
    public int spreadPDAP;

    [Header("Current Ability Points")]
    public int current_playerHPAP;
    public int current_playerRegenAP;
    public int current_playerShieldAP;
    public int current_spawnAddAP;
    public int current_spFactorAP;
    public int current_playerSSAP;
    public int current_comboTimerMaxAP;
    public int current_gunFRAP;
    public int current_gunPSAP;
    public int current_gunPDAP;
    public int current_spreadFRAP;
    public int current_spreadPSAP;
    public int current_spreadPDAP;

    [Header("Stats")]
    [Header("Gun Stats")]
    public float gunStartingPS;
    public float gunStartingFR;
    public float gunStartingPD;
    public int defaultGunKills;
    public int spreadGunKills;
    public int splashGunKills;
    public int defaultGunShotsFired;
    public int spreadGunShotsFired;
    public int splashGunShotsFired;
    public int totalShotsFired;

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

    [Header("Stage Stats")]
    public int highestStage;
    public int startingStage;

    [Header("Enemy Stats")]
    public int enemiesDefeated;
    public int shootersDefeated;

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
