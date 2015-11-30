﻿using UnityEngine;
using System.Collections;

public class EnableOnFirstTime : MonoBehaviour {

    public GameObject FirstTimeObj;

	// Use this for initialization
	void Awake () {
        if (PlayerPrefs.GetInt("IsFirstTime", 0) == 0)
        {
            FirstTimeObj.SetActive(true);
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<MusicManager>().SetMusic(4);
        }
        else
        {
            Debug.Log("I MAKE BAD TIME" + gameObject.name + "" + PlayerPrefs.GetInt("IsFirstTime", 0));
            
            FirstTimeObj.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
