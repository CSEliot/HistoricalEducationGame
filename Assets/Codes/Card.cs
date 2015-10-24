using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    private string Name;
    private string FlavorText;
    private Image Graphic;
    private bool IsSpecial;
    //private delegate void PrintyFunction();
    public Action SpecialAbility;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    }

    public void AssignData(Image graphic, string name, string flavorText, bool isSpecial)
    {
        Graphic = graphic;
        Name = name;
        FlavorText = flavorText;
        IsSpecial = isSpecial;
    }


}
