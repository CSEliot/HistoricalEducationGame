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
    private bool IsEvent;
    private TurnManager MyManager;

    private int Type; //+1, +2, stop, etc.
    private int NumPos;
    private bool IsPlayerOwned;
    private bool OnField;

    // Use this for initialization
    void Start () {
        OnField = false;
        //normally wouldn't give card connection to manager, but we're
        //short on time . . .
        MyManager = GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>();
    }
    
    // Update is called once per frame
    void Update () {
        if (transform.localPosition.magnitude != 0)
        {
            transform.localPosition = Vector3.zero;
        }
    }

    public void AssignData(Sprite graphic, string name, string flavorText, 
        bool isSpecial, bool isEvent, int type, bool isAI)
    {
        Graphic.sprite = graphic;
        Name.text = name;
        FlavorText.text = flavorText;
        IsSpecial = isSpecial;
        IsEvent = isEvent;
        Type = type;
        IsPlayerOwned = !isAI;
    }

    public void Activated()
    {
        if (transform.parent.name.Contains("Hand"))
        {
            //only cards in hand can be played.
            MyManager.HandPlayed(transform, NumPos);
            OnField = true;
            GameObject.FindGameObjectWithTag("SFXController").GetComponent
                <SoundEffectManager>().PlaySound(4);
        }
        else
        {
            OnDestroy();
            Destroy(gameObject, 0.05f);
            MyManager.CardSpotChosen(NumPos);
            GameObject.FindGameObjectWithTag("SFXController").GetComponent
                <SoundEffectManager>().PlaySound(5);
        }
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
        //Debug.Log("I am a card being destroyed! Type: " + Type);
        //switch(Type)
        //{
        //    case 6:
        //    case 4:
                             
        //        break;
        //    default:
        //        Debug.Log("Uncaught type num: " + Type);
        //        break;
        //}
        //Debug.Log("Destroyed, Unstopping: " + NumPos);
        if (IsPlayerOwned && !IsEvent)
        {
            //GetAIField
            //Debug.Log("Unstopping AI card . . .");
            MyManager.GetField(false).UnStop(NumPos);
        }
        else if (!IsPlayerOwned && !IsEvent)
        {
            //get player field
            //Debug.Log("Unstopping Player card . . .");
            MyManager.GetField(true).UnStop(NumPos);
        }   
    }

    public void SpecialAbility()
    {
        GameObject temp = GameObject.FindGameObjectWithTag("SFXController");

        temp.GetComponent<AudioSource>().pitch = 1 + ((float)NumPos / 5f)*2f; 
        temp.GetComponent<SoundEffectManager>().PlaySound(12);
        temp.GetComponent<AudioSource>().pitch = 1;

        AllSpecialFunctions.ActivateAbility(this);
    }

    public int GetCardType()
    {
        return Type;
    }

    public bool GetIsPlayerOwned()
    {
        return IsPlayerOwned;
    }
}
