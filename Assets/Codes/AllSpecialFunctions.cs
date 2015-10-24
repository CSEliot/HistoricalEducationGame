using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class AllSpecialFunctions : MonoBehaviour {

	public Dictionary<int, Action> SpecialAbilityList; 

	// Use this for initialization
	void Start () {
		SpecialAbilityList = new Dictionary<int, Action> ();
		SpecialAbilityList.Add(1, () => PlusOne());
        SpecialAbilityList.Add(2, () => PlusTwo());
        SpecialAbilityList.Add(3, () => PlusThree());
        SpecialAbilityList.Add(4, () => PlusTwo());
        SpecialAbilityList.Add(5, () => Clear());
        SpecialAbilityList.Add(6, () => Stop());
        SpecialAbilityList.Add(7, () => Double());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator PlusOne(){
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(1);
        yield return new WaitForSeconds(0.3f);
	}

    IEnumerator PlusTwo()
    {
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(2);
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator PlusThree()
    {
        GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>().IncreaseInfluence(1);
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator Clear()
    {
        GameObject.FindGameObjectWithTag("PlayFieldAI").
            GetComponent<PlayField>().Clear();
        GameObject.FindGameObjectWithTag("PlayFieldYours").
            GetComponent<PlayField>().Clear();
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(0.3f);
    }

    IEnumerator Double()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
