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
        CheckSpreadUnlocked();
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

    public void UnlockSpread(){
        BuyGunButton bgb = GetComponentInParent<BuyGunButton>();
        print(gmi.lStats.spendablePoints + " " + bgb.cost);
        if (gmi.lStats.spendablePoints >= bgb.cost)
        {
            gmi.lStats.spendablePoints -= bgb.cost;
            gmi.lStats.spreadUnlocked = true;
            bgb.gameObject.SetActive(false);
        }
    }

    public void CheckSpreadUnlocked(){
        BuyGunButton bgb = GetComponentInParent<BuyGunButton>();
        if (gmi.lStats.spreadUnlocked && bgb)
        {
            bgb.gameObject.SetActive(false);
        }
    }
}
