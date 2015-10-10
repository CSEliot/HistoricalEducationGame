using UnityEngine;
using System.Collections;

public class ResearchReturn : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Return()
    {
        GameObject.Find("5 - ResearchGame").SetActive(false);
    }
}
