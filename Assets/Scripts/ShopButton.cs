using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

    public int cost;
    public int level;
    public Text[] objTexts;

    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        objTexts = GetComponentsInChildren<Text>();

        cost = gmi.GetStat("cost_" + name);
        level = gmi.GetStat(name);
        UpdateStatDisplay();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void UpdateStatDisplay(){
        UpdateCostLevel();
        objTexts[1].text = "" + cost;
        print(cost + " Cost"); 
        objTexts[2].text = "Lv." + level;
    }

    public void UpdateCostLevel(){
        cost = gmi.GetStat("cost_" + name);
        level = gmi.GetStat(name);
    }
}
