using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour {

    public int currentPoints = 0;

    private Text cpText;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        cpText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        cpText.text = "Points : " + gmi.lStats.currentPoints;
	}

    public void CalculatePoints()
    {
        gmi.lStats.spThisRound = gmi.lStats.currentPoints / 5;
        gmi.lStats.spendablePoints += gmi.lStats.spThisRound;print(gmi.lStats.spendablePoints);
    }
}
