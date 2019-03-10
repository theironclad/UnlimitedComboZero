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
        sd = gmi.GetComponent<StatDescriptions>();
        managerTexts = GetComponentsInChildren<Text>();
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
        string currentString = "current_"+statString;
        int currentValue = gmi.GetStat(currentString);
        int normalValue = gmi.GetStat(statString);
        if(currentValue>0){
            statSlider.value = currentValue;
            print("Stat slider value " + statSlider.value);
            print("Set string to current : " + currentString + " " + currentValue);
        }else if (currentValue<=0 && normalValue==1)
        {
            gmi.SetStat(currentString, 1);
            print("Setting to stat string");
            statSlider.value = normalValue;

        }else if (currentValue <= 0 && normalValue > 1)
        {
            gmi.SetStat(currentString, normalValue);
            print("Setting to stat string");
            statSlider.value = normalValue;
        }else{
            print("Setting to current string");
            statSlider.value = currentValue;
        }
        print(statString + " From SetupSlider");
        statSlider.maxValue = normalValue;
    }

    public void CloseSlider(){
        int value = (int)statSlider.value;       
        gmi.SetStat("current_" + statString, value);
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
        currentStatString = statName;
        SetupText(statName);
        SetupSlider(statName);
    }

    public void SetupText(string stat){
        string sString = stat.Replace("player", "");
        sString = sString.Replace("spawn", "");
        sString = sString.Replace("_obj", "");
        sString = sString.Replace("AP", "");
        print(sString);
        print(stat);
        if (sString.Contains("gun"))
        {
            sString = sString.Replace("gun", "");
        }
        if (sString.Contains("spread"))
        {
            sString = sString.Replace("spread", "");
        }
        switch (sString){
            case "Add" :
                managerTexts[0].text = "Spawn Add";
                break;
            case "comboTimerMax":
                managerTexts[0].text = "Combo Timer Max";
                break;
            case "spFactor":
                managerTexts[0].text = "SP Bonus";
                break;
            case "SS":
                managerTexts[0].text = "Stage Bonus";
                break;
            case "FR":
                managerTexts[0].text = "Fire Rate";
                break;
            case "PS":
                managerTexts[0].text = "Bullet Speed";
                break;
            case "PD":
                managerTexts[0].text = "Bullet Damage";
                break;
            default :
                managerTexts[0].text = sString;
                break;
        }
        gmi.GetStat(stat);
        managerTexts[3].text = gmi.GetStat("cost_" + stat).ToString();
        managerTexts[1].text = sd.Descriptions[sString];
        managerTexts[4].text = gmi.GetStat("current_" + stat).ToString();
        managerTexts[6].text = gmi.GetStat(stat).ToString();
    }
}
