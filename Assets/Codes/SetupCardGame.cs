using UnityEngine;
using System.Collections;

public class SetupCardGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void StartGame(int StageNum) {
        GameObject.FindGameObjectWithTag("MenuController").GetComponent<AllMenuNav>().ChangeSceneTo(4);
    }
}
