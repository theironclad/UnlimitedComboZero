using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointsController : MonoBehaviour {

    public int currentPoints = 0;
    public float spFactor = 5;

    private Text cpText;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        cpText = GetComponent<Text>();
        if (gmi.lStats.spFactor<5.0)
        {
            spFactor = gmi.lStats.spFactor;
        }
    }
	
	// Update is called once per frame
	void Update () {
        cpText.text = "" + gmi.lStats.currentPoints;
	}

    public void CalculatePoints()
    {
        gmi.lStats.spThisRound = Mathf.RoundToInt(gmi.lStats.currentPoints / spFactor);
        if (gmi.lStats.spThisRound <=0)
        {
            gmi.lStats.spThisRound = 1;
        }
        gmi.lStats.spendablePoints += gmi.lStats.spThisRound;
    }
}
