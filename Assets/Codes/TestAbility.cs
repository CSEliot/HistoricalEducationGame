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
        if (Input.GetKeyDown("3"))
        {
            AllSpecialFunctions.TestAbility(17, 0);
        }
        if (Input.GetKeyDown("4"))
        {
            AllSpecialFunctions.TestAbility(18, 0);
        }
        if (Input.GetKeyDown("5"))
        {
            AllSpecialFunctions.TestAbility(19, 0);
        }
        if (Input.GetKeyDown("6"))
        {
            AllSpecialFunctions.TestAbility(20, 0);
        }
	}
}
