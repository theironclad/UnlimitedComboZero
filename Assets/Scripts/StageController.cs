using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour {
    
    private GameManager gmi;
    private Text csText;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        csText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        csText.text = "Stage : " + gmi.lStats.currentStage;
	}
}
