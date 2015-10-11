using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResearchButton : MonoBehaviour {

    public GameObject ResearchMenu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Return()
    {
        ResearchMenu.SetActive(false);
    }

    public void GoToLearnScreen()
    {
        ResearchMenu.SetActive(true);
    }
}
