using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatButton : MonoBehaviour
{

    public string buttonName;
    public ShopController sc;
    public StatManager sm;

    // Use this for initialization
    void Start()
    {
        buttonName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CloseSM(){
        buttonName = transform.parent.name;
        string toClose = buttonName.Replace("_StatManager","");
        sm.CloseSlider();
        sc.StatManagerManager(toClose, "close");
    }
}
