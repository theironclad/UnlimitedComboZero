using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StatDescriptions : MonoBehaviour {

    public Dictionary<string, string> Descriptions;

    public void Start()
    {
        Descriptions = new Dictionary<string, string>();

        Descriptions.Add("HP", "Each point put into HP will add 10 HP to your player.");
        Descriptions.Add("Regen", "Each point put into HP Regen increases health regen by .1 per second");
        Descriptions.Add("FR", "Each point put into Fire Rate will increase rate of fire by .1 / second.");
        Descriptions.Add("PS", "Each point put into Bullet Speed will increase projectile speed.");
        Descriptions.Add("PD", "Each point put into Bullet Damage will increase projectile damage");
        Descriptions.Add("Add", "Each point in Spawn Add will spawn an additional enemy for each enemy kill");
        Descriptions.Add("comboTimerMax", "Each point in Combo Timer Max will increase the time before combos expire.");
        Descriptions.Add("SS","Each point in Stage + will increase the starting stage by 1.");
        Descriptions.Add("spFactor", "Each point in SP Bonus will increase SP multiplier by .01.");
    }
}
