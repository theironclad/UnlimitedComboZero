using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour {

	public Text[] managerTexts;
    public StatDescriptions sd;
    public Slider statSlider;

    //private string statString;
    public string statString;
    public string currentStatString;

    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        managerTexts = GetComponentsInChildren<Text>();
        //statString = name.Replace("_StatManager", "");
        NewSetupManager(statString);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSlider();
        if (statString!=currentStatString)
        {
            NewSetupManager(statString);
        }
    }

    public void SetupSlider(string statString){
        print(statString + " From SetupSlider");
        statSlider.maxValue = gmi.GetStat(statString);
        statSlider.value = gmi.GetStat("current_" + statString);
    }

    public void CloseSlider(){
        int value = (int)statSlider.value;
        gmi.SetStat(statString, value);
    }

    private void UpdateSlider(){
        managerTexts[4].text = statSlider.value.ToString();
    }

    public void UpgradeStat(){
        int currentValue = gmi.GetStat(statString);
        if (gmi.GetStat("cost_" + statString)<=gmi.lStats.spendablePoints) 
        {
            print(statString + " is target stat");
            gmi.ModifyStat(statString, 1);
            gmi.ModifyStat("cost_" + statString, (currentValue * 2));
            gmi.lStats.spendablePoints -= gmi.GetStat("cost_" + statString);
            NewSetupManager(statString);
        }
    }

    public void NewSetupManager(string statName){      
        SetupText(statName);
        SetupSlider(statName);
        currentStatString = statName;
    }

    public void SetupText(string stat){
        string sString = stat.Replace("player", "");
        sString = sString.Replace("AP", "");
        managerTexts[0].text = sString;
        gmi.GetStat(stat);
        managerTexts[3].text = gmi.GetStat("cost_" + stat).ToString();
        managerTexts[1].text = sd.Descriptions[sString];
        managerTexts[4].text = gmi.GetStat("current_" + stat).ToString();
        managerTexts[6].text = gmi.GetStat(stat).ToString();
    }

}
