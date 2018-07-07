using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyGunButton : MonoBehaviour {
    public int cost;
    public Text[] objTexts;

    private GameManager gmi;

    // Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        objTexts = GetComponentsInChildren<Text>();
        cost = gmi.GetStat("cost_" + name);
        objTexts[1].text = ""+cost;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
