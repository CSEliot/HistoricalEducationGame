using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckPassword : MonoBehaviour {

    private string password;

    // Use this for initialization
    void Start () {
        if (Application.loadedLevelName.Contains("Pop"))
        {
            password = "shoe";
        }
        else
        {
            password = "cup";
        }
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
