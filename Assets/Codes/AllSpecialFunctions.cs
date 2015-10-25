using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class AllSpecialFunctions : MonoBehaviour {

	public Dictionary<int, Action> SpecialAbilityList; 

	// Use this for initialization
	void Start () {
		SpecialAbilityList = new Dictionary<int, Action> ();
		SpecialAbilityList.Add(0, () => PlusOne());
        SpecialAbilityList.Add(1, () => PlusTwo());
        SpecialAbilityList.Add(2, () => PlusThree());
        SpecialAbilityList.Add(3, () => Clear());
        SpecialAbilityList.Add(4, () => Stop());
        SpecialAbilityList.Add(5, () => Double());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void PlusOne()
    {
        Go.Do("+1 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(1);
	}

    public void PlusTwo()
    {
        Go.Do("+2 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(2);
    }

    public void PlusThree()
    {
        Go.Do("+3 card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(3);
    }

    public void Clear()
    {
        Go.Do("Clear card called!");
        GameObject.FindGameObjectWithTag("PlayFieldAI").
            GetComponent<PlayField>().Clear();
        GameObject.FindGameObjectWithTag("PlayFieldYours").
            GetComponent<PlayField>().Clear();
    }

    public void Stop()
    {
        Go.Do("Stop card called!");
        //int thisCardPos = gameObject.GetComponent<Card>().GetNumPos();
        //GameObject.FindGameObjectWithTag("TurnManager").
        //    GetComponent<TurnManager>().GetInactiveField().
        //    Stop(thisCardPos);
    }

    public void Double()
    {
        Go.Do("Double card called!");
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().DoubleNext();
    }
}
