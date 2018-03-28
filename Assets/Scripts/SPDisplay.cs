using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SPDisplay : MonoBehaviour {

    public int sPCurrent;

    private Text displayText;
    private PointsController pc;

    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        pc = FindObjectOfType<PointsController>();
        displayText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        displayText.text = "SP : " + gmi.lStats.spendablePoints;
	}
}
