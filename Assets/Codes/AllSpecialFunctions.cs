using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class AllSpecialFunctions : MonoBehaviour {

    public Dictionary<int, Action> SpecialAbilityList; 

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    
    }


    public static void TestAbility(int cardType, int numPos)
    {
        GameObject temp = GameObject.Find("DeckYours").
            GetComponent<Deck>().BuildClone(cardType);
        temp.GetComponent<Card>().SetNumPos(numPos);
        ActivateAbility(temp.GetComponent<Card>());
    }

    public static void ActivateAbility(Card ActivatedCard)
    {
        int cardType = ActivatedCard.GetCardType();
        Debug.Log("Card Activated: " + cardType);
        switch(cardType){
            case 0:
                PlusOne();
                break;
            case 1:
                PlusTwo();
                break;
            case 2:
                PlusThree();
                break;
            case 3:
                Clear(true, true);
                break;
            case 4:
                Stop(ActivatedCard.GetNumPos());
                break;
            case 5:
                Double();
                break;
            case 6:
                Stop(ActivatedCard.GetNumPos());
                PlusOne();
                Triple();
                break;
            case 7:
                ConvertAllCardsTo(0);
                break;
            case 8:
                IncreaseMultipliers(2);
                break;
            case 9:
                SplitField(0, 5);
                break;
            case 10:
                GameObject.FindGameObjectWithTag("InfluenceManager").
                    GetComponent<InfluenceManager>().SetInfluence(15);
                break;
            case 11:
                //ignore stops somehow
                PlusTwo();
                Stop(ActivatedCard.GetNumPos());
                break;
            case 12:
                SetDelayedAbility(!ActivatedCard.GetIsPlayerOwned(), 4, 3);
                break;
            case 13:
                SplitField(0, 0);
                break;
            case 14:
                Stop(ActivatedCard.GetNumPos());
                PlusTwo();
                Double();
                break;
            case 15:
                PlusFiveOnce();
                break;
            case 16:
                PlusFour();
                break;
            case 17:
                ChooseBetween();
                break;
            case 18:
                SplitField(4, -1);
                break;
            case 19:
                ToggleInfluenceBoost();
                break;
            case 20:
                TogglePermaInfluenceBoost(!ActivatedCard.GetIsPlayerOwned());
                break;
            case 21:
                //this is specifically only called by one other card ability.
                Clear(false, true);
                break;
            default:
                Debug.Log("Card Type Unhandled: " + cardType);
                break;

        }
    }

    private static void TogglePermaInfluenceBoost(bool forAI)
    {
        GameObject.FindGameObjectWithTag("TurnManager").
                GetComponent<TurnManager>().
                EnablePermaInfluenceBoost(forAI);
    }

    private static void ToggleInfluenceBoost()
    {
        GameObject.FindGameObjectWithTag("InfluenceManager").
                GetComponent<InfluenceManager>().EnableSpoilsInfluence();
    }

    /// <summary>
    /// To be used exclusively when using SplitField to set a whole
    /// field to all Stops.
    /// </summary>
    /// <param name="yourField"></param>
    /// <param name="opponentField"></param>
    private static void DisableField(bool yourField, bool opponentField)
    {
        if (yourField)
        {
            GameObject.FindGameObjectWithTag("TurnManager").
                GetComponent<TurnManager>().GetActiveField().DisableAll();
        }
        if (opponentField)
        {
            GameObject.FindGameObjectWithTag("TurnManager").
                GetComponent<TurnManager>().GetInactiveField().DisableAll();
        }
    }

    private static void ChooseBetween()
    {
        GameObject.FindGameObjectWithTag("CardChooseEvent").
            transform.GetChild(0).gameObject.SetActive(true);
    }

    private static void SetDelayedAbility(bool forAI, int turnNum, int abilityNum)
    {
        GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>().SetNextAbility(forAI, turnNum, abilityNum);
    }

    /// <summary>
    /// Replaces each field with each card type.
    /// if either parameter is -1 then that side is ignored..
    /// </summary>
    /// <param name="cardType1"> Your field becomes this type.</param>
    /// <param name="cardType2"> Opponent field becomes this type.</param>
    private static void SplitField(int cardType1, int cardType2){
        if (cardType1 != -1)
        {
            GameObject.FindGameObjectWithTag("TurnManager").
                GetComponent<TurnManager>().GetActiveField().
                ConvertWholeFieldTo(cardType1);
        }
        if (cardType2 != -1)
        {
            GameObject.FindGameObjectWithTag("TurnManager").
                GetComponent<TurnManager>().GetInactiveField().
                ConvertWholeFieldTo(cardType2);
        }
    }

    private static void IncreaseMultipliers(int amount)
    {
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().SetSpecialMod(amount);
    }

    private static void ConvertAllCardsTo(int cardType)
    {
        GameObject.FindGameObjectWithTag("PlayFieldYours").
            GetComponent<PlayField>().ConvertAllCardsTo(cardType);
        GameObject.FindGameObjectWithTag("PlayFieldAI").
            GetComponent<PlayField>().ConvertAllCardsTo(cardType);
    }

    private static void PlusOne()
    {
        //Debug.Log("+1 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(1);
    }

    private static void PlusTwo()
    {
        //Debug.Log("+2 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(2);
    }

    private static void PlusThree()
    {
        //Debug.Log("+3 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(3);
    }

    private static void PlusFour()
    {
        //Debug.Log("+3 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(4);
    }

    private static void PlusFiveOnce()
    {
        //Debug.Log("+3 card called!");
        if (GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>().
            GetActiveField().GetIf15Activated())
        {
            GameObject.FindGameObjectWithTag("InfluenceManager").
                GetComponent<InfluenceManager>().DecreaseInfluence(5);
        }
        else
        {
            GameObject.FindGameObjectWithTag("InfluenceManager").
                GetComponent<InfluenceManager>().IncreaseInfluence(5);
        }
    }

    private static void Clear(bool yourSide, bool opponentSide)
    {
        if (yourSide)
        {
            GameObject.FindGameObjectWithTag("PlayFieldYours").
                GetComponent<PlayField>().Clear();
            GameObject.FindGameObjectWithTag("ClearText").transform.
                GetChild(0).gameObject.SetActive(true);
        }
        if (opponentSide)
        {
            //Debug.Log("Clear card called!");
            GameObject.FindGameObjectWithTag("PlayFieldAI").
                GetComponent<PlayField>().Clear();
            GameObject.FindGameObjectWithTag("ClearText").transform.
                GetChild(1).gameObject.SetActive(true);
        }
    }

    private static void Stop(int num)
    {
        //Debug.Log("Stop card called!: " + num);
        
        GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>().GetInactiveField().
            Stop(num);
    }

    private static void Double()
    {
        //Debug.Log("Double card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().DoubleNext();
    }

    private static void Triple()
    {
        Debug.Log("Triple card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().TripleNext();
    }
}
