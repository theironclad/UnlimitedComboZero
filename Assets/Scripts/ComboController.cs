using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboController : MonoBehaviour {
    
    public bool ctRunning = false;
    public float comboTimer =0.0f;
    public float comboTimerMax = 8f;
    public float comboTimeRenew = 5.0f;
    public int comboCount=0;

    private Text cText;
    private GameManager gmi;
	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        cText = GetComponent<Text>();
        if(gmi.lStats.currentCombo>0){
            comboTimer = gmi.lStats.currentComboTimer;
            comboCount = gmi.lStats.currentCombo;
            ctRunning = true;
        }

        if (gmi.lStats.comboTimerMaxAP>=1)
        {
            comboTimerMax += comboTimerMax * 0.1f;
        }
    }
	
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
        cText.text = comboCount + " COMBO";
    }

    public void StartComboTimer(){
        if (ctRunning)
        {
            if ((comboTimer + comboTimeRenew) >=comboTimerMax)
            {
                comboTimer = comboTimerMax;
            }else{
                comboTimer += comboTimeRenew;
            }
            comboCount++;
        }else{
            ctRunning = true;
            comboTimer += comboTimeRenew;
            comboCount++;
        }
    }
}
