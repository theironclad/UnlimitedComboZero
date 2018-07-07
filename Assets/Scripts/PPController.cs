using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PPController : MonoBehaviour {


    const string MASTER_VOLUME_KEY = "master_volume";
    const string SFX_VOLUME_KEY = "sfx_volume";
    const string BGM_VOLUME_KEY = "bgm_volume";

    public static void SetMasterVolume(int volume){
        if (volume>=0 && volume <= 100)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
    }

    public static int GetMasterVolume(){
        return PlayerPrefs.GetInt(MASTER_VOLUME_KEY);
    }

    public static void SetSFXVolume(int volume){
        if (volume>=0 && volume <=100)
        {
            PlayerPrefs.SetInt(SFX_VOLUME_KEY, volume);
        }
    }

    public static int GetSFXVolume(){
        return PlayerPrefs.GetInt(SFX_VOLUME_KEY);
    }

    public static void SetBGMVolume(int volume){
        if (volume>=0f && volume<=100)
        {
            PlayerPrefs.SetInt(BGM_VOLUME_KEY, volume);
        }
    }

    public static int GetBGMVolume(){
        return PlayerPrefs.GetInt(BGM_VOLUME_KEY);
    }

}
