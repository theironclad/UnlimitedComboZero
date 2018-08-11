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
    }
}
