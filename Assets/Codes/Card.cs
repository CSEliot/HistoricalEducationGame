using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class Card : MonoBehaviour {

	private int Influence;
	private string Name;
	private string FlavorText;
	private Image Graphic;
	private bool IsSpecial;
	//private delegate void PrintyFunction();
	public Action SpecialAbility;

	// Use this for initialization
	void Start () {
        if(IsSpecial)
		SpecialAbility = GameObject.Find ("Main Camera").GetComponent<AllSpecialFunctions> ().SpecialAbilityList [1];
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void AssignData(int Influence, string Name, string FlavorText)
    {

    }


}
