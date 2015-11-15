using UnityEngine;
using System.Collections;

public class EnableOnFirstTime : MonoBehaviour {

    public GameObject FirstTimeObj;

	// Use this for initialization
	void Awake () {
        if (PlayerPrefs.GetInt("IsFirstTime") == 0 ||
            PlayerPrefs.GetInt("IsFirstTime") == null)
        {
            FirstTimeObj.SetActive(true);
        }
        else
        {
            FirstTimeObj.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
