﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthController : MonoBehaviour {

    public Text playerHealthText;

    private PlayerController pc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        playerHealthText = GetComponent<Text>();
        pc = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        playerHealthText.text = "" + pc.playerHP;
	}
}
