using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour {

    public string goName;
    public Text thisText;
    private GameManager gmi;

	// Use this for initialization
	void Start () {
        gmi = GameManager.Instance;
        goName = transform.parent.name;
	}
	
	// Update is called once per frame
	void Update () {
        SetUpText();
	}

    public void SetUpText(){

        switch(goName){
            case "defaultGun":
                thisText.text = "Total Kills : " + gmi.lStats.defaultGunKills.ToString();
                break;
            case "spreadGun":
                thisText.text = "Total Kills : " + gmi.lStats.spreadGunKills.ToString();
                break;
            case"splashGun":
                thisText.text = "Total Kills : " + gmi.lStats.splashGunKills.ToString();
                break;
            default:
                break;
        }
    }
}
