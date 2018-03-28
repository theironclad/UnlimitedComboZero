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

    [Header("Gun Stats")]
    public float gunStartingPS;
    public float gunStartingFR;
    public float gunStartingPD;

    [Header("Gun Abilities")]
    public int gunFRAP;
    public int gunPSAP;
    public int gunPDAP;

    [Header("Ability Point Stats")]
    public int spThisRound;
    public int spendablePoints;

    [Header("Combo Stats")]
    public int highestCombo;
    public float longestCombo;

    [Header("Stage Stats")]
    public int highestStage;

    [Header("Enemy Stats")]
    public int enemiesDefeated;

    [Header("Shop Stats")]
    public int totalPurchases;
    public int cost_playerHPAP;
    public int cost_gunFRAP;
    public int cost_gunPSAP;
    public int cost_gunPDAP;

    [Header("Player Current")]
    public float currentPlayerHP;

    [Header("Gun Current")]
    public float currentGunPS;
    public float currentGunFR;
    public float currentGunPD;

    [Header("Game Current")]
    public int currentPoints;
    public int currentStage;

}
