using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour {

    public GameObject[] upgradeShops;
    public GameObject[] statManagers;

    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StatManagerManager(string statName, string call)
    {
        if (call=="open")
        {
            print("opening mm");
            upgradeShops[0].SetActive(false);
            statManagers[0].SetActive(true);
            statManagers[0].GetComponent<StatManager>().statString = statName;
        }

        if(call=="close")
        {
            print("Closing SMM");
            upgradeShops[0].SetActive(true);
            statManagers[0].SetActive(false);
        }
    }

}
