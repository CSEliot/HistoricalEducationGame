using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckPasswordExport : MonoBehaviour {

    public string password;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Enter()
    {   
        if (GameObject.FindGameObjectWithTag("PasswordField")
            .GetComponent<Text>().text == "shoe")
        {
            GameObject.FindGameObjectWithTag("MenuController").
            GetComponent<AllMenuNav>().ChangeSceneTo(1);
        }
    }
}
