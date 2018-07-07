using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComboSlider : MonoBehaviour {

    private Slider comboSlider;
    private ComboController cc;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        comboSlider = GetComponent<Slider>();
        cc = FindObjectOfType<ComboController>();
        comboSlider.maxValue = cc.comboTimerMax;
        comboSlider.value = cc.comboTimer;
	}
	
	// Update is called once per frame
	void Update () {
        if (cc.ctRunning)
        {
            comboSlider.value = cc.comboTimer;
        }
	}

}
