using UnityEngine;
using System.Collections;

public class TestAbility : MonoBehaviour {

    public int testLocation;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("1"))
        {
            AllSpecialFunctions.TestAbility(7, 0);
        }
        if (Input.GetKeyDown("2"))
        {
            AllSpecialFunctions.TestAbility(9, 0);
        }
	}
}
