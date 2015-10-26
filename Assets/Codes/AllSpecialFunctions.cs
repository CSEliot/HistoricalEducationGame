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

    public static void ActivateAbility(Card ActivatedCard)
    {
        int cardType = ActivatedCard.GetCardType();
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
                Clear();
                break;
            case 4:
                Stop(ActivatedCard.GetNumPos());
                break;
            case 5:
                Double();
                break;
            default:
                Debug.Log("Card Type Unhandled: " + cardType);
                break;

        }
    }

    private static void PlusOne()
    {
        Debug.Log("+1 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(1);
	}

    private static void PlusTwo()
    {
        Debug.Log("+2 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(2);
    }

    private static void PlusThree()
    {
        Debug.Log("+3 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(3);
    }

    private static void Clear()
    {
        Debug.Log("Clear card called!");
        GameObject.FindGameObjectWithTag("PlayFieldAI").
            GetComponent<PlayField>().Clear();
        GameObject.FindGameObjectWithTag("PlayFieldYours").
            GetComponent<PlayField>().Clear();
        GameObject.FindGameObjectWithTag("ClearText").transform.
            GetChild(0).gameObject.SetActive(true);
        GameObject.FindGameObjectWithTag("ClearText").transform.
            GetChild(1).gameObject.SetActive(true);
    }

    private static void Stop(int num)
    {
        Debug.Log("Stop card called!: " + num);
        
        GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>().GetInactiveField().
            Stop(num);
    }

    private static void Double()
    {
        Debug.Log("Double card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().DoubleNext();
    }
}
