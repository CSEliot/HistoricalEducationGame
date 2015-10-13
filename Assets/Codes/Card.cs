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
	private delegate void PrintyFunction();
	Action NewFunction;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		NewFunction = GameObject.Find ("Main Camera").GetComponent<AllSpecialFunctions> ().SpecialAbilityList [1];
		NewFunction ();
	}


}
