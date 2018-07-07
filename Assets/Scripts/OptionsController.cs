using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsController : MonoBehaviour {

    public GameObject optionsObj;

    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider bgmSlider;

    public AudioMixer am;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        optionsObj = gameObject;    
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void MuteAll(){
        masterSlider.value = -80;
        sfxSlider.value = -80;
        bgmSlider.value = -80;
    }

    public void ResetToDefaults(){
        masterSlider.value = -10;
        sfxSlider.value = -10;
        bgmSlider.value = -10;
    }

    public void SaveAndExit(){
        GameObject.Find("Options_Panel").SetActive(false);
    }

    public void GetSliders(){
        Slider[] sliders = GetComponentsInChildren<Slider>();
        masterSlider = sliders[0];
        sfxSlider = sliders[1];
        bgmSlider = sliders[2];
    }   

    public void OpenOptions(){
        optionsObj.transform.GetChild(0).gameObject.SetActive(true);
        GetSliders();
        PopulateSliderValues();
    }

    public void SetSFXLevel(float sfxLvl){
        am.SetFloat("sfxVolume",sfxLvl);
        gmi.lStats.sfxVolumeSet = sfxLvl;
    }

    public void SetBGMLevel(float bgmLvl){
        am.SetFloat("bgmVolume", bgmLvl);
        gmi.lStats.bgmVolumeSet = bgmLvl;
    }

    public void SetMasterLevel(float masterLvl){
        am.SetFloat("masterVolume", masterLvl);
        gmi.lStats.masterVolume = masterLvl;
    }

    public void PopulateSliderValues(){
        masterSlider.value = gmi.lStats.masterVolume;
        sfxSlider.value = gmi.lStats.sfxVolumeSet;
        bgmSlider.value = gmi.lStats.bgmVolumeSet;
    }
}
