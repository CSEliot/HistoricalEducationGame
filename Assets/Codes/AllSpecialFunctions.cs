using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class AllSpecialFunctions : MonoBehaviour {

	public Dictionary<int, Action> SpecialAbilityList; 

	// Use this for initialization
	void Start () {
		SpecialAbilityList = new Dictionary<int, Action> ();
		SpecialAbilityList.Add(1, ()=>TestMe());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TestMe(){
		print ("pwep");
	}
}
