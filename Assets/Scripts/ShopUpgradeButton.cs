using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShopUpgradeButton : MonoBehaviour {


    private ShopButton sb;

    private GameManager gmi;

	// Use this for initialization
	void Start () {
        sb = GetComponentInParent<ShopButton>();
        gmi = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UpgradeStat(){
        print(sb.cost + "SB Cost");
        if (gmi.lStats.spendablePoints>=sb.cost)
        {
            string parentString = transform.parent.name;
            int currentValue = gmi.GetStat(parentString);
            gmi.lStats.spendablePoints -= sb.cost;
            gmi.ModifyStat(parentString, 1);
            gmi.ModifyStat("cost_" + parentString, (int)(currentValue * 2));
            sb.UpdateStatDisplay();
        }
    }
}
