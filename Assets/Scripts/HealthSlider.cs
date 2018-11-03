using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour {

    private PlayerController pc;
    private Slider healthSlider;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        healthSlider = GetComponent<Slider>();
        pc = FindObjectOfType<PlayerController>();
        Invoke("SetUpSlider", .3f);
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSlider();
	}

    void SetUpSlider(){
        healthSlider.maxValue = gmi.lStats.currentPlayerHP;
        healthSlider.value = pc.playerHP;
    }

    void UpdateSlider(){
        if (pc)
        {
            healthSlider.value = pc.playerHP;
        }else{
            healthSlider.value = 0;
        }
    }
}
