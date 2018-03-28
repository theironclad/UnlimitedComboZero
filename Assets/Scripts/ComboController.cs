using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour {
    
    public bool ctRunning = false;
    public float comboTimer =0.0f;
    public float comboTimeRenew = 5.0f;
    public int comboCount=0;

    private Text cText;
    private GameManager gmi;
	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        cText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (comboTimer>=0.0f)
        {
            comboTimer -= Time.deltaTime;
        }
        if (comboTimer<=0.0f)
        {
            ctRunning = false;
            comboCount = 0;
        }
        cText.text = "Combo : " + comboCount;
    }

    public void StartComboTimer(){
        if (ctRunning)
        {
            comboTimer += comboTimeRenew;
            comboCount++;
        }else{
            ctRunning = true;
            comboTimer += comboTimeRenew;
            comboCount++;
        }
    }
}
