using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    public Text Name;
    public Text FlavorText;
    public Image Graphic;
    private bool IsSpecial;
    //private delegate void PrintyFunction();
    public Action SpecialAbility;
    private bool IsEvent;
    private TurnManager MyManager;

    private int Type; //+1, +2, stop, etc.
    private int NumPos;

    // Use this for initialization
    void Start () {
        //normally wouldn't give card connection to manager, but we're
        //short on time . . .
        MyManager = GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>();
    }
    
    // Update is called once per frame
    void Update () {
    }

    public void AssignData(Sprite graphic, string name, string flavorText, 
        bool isSpecial, bool isEvent, int type)
    {
        Graphic.sprite = graphic;
        Name.text = name;
        FlavorText.text = flavorText;
        IsSpecial = isSpecial;
        IsEvent = isEvent;
        Type = type;
    }

    public void Activated()
    {
        MyManager.HandPlayed(transform, NumPos);
    }

    public bool GetIsEvent()
    {
        return IsEvent;
    }

    public int GetNumPos()
    {
        return NumPos;
    }

    public void SetNumPos(int pos)
    {
        NumPos = pos;
    }

    void OnDestroy()
    {
        switch(Type)
        {
            case 4:
                //stops should unstop when removed:
                MyManager.GetInactiveField().UnStop(NumPos);
                break;
            default:
                Go.Do("Uncaught type num: " + Type);
                break;
        }
    }


}
