using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CheckPassword : MonoBehaviour {

    private string password;

    // Use this for initialization
    void Start () {
        if (SceneManager.GetActiveScene().name.Contains("AB"))
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
        //GameObject.FindGameObjectWithTag("PasswordField")
        //    .GetComponent<Text>().text = "ddd";
    }

    public void Enter()
    {   
        if (GameObject.FindGameObjectWithTag("PasswordField")
            .GetComponent<Text>().text == password)
        {
            GameObject.FindGameObjectWithTag("PasswordField")
            .GetComponent<Text>().gameObject.transform.parent
            .GetComponent<InputField>().text = "";
            GameObject.FindGameObjectWithTag("MenuController").
            GetComponent<AllMenuNav>().ChangeSceneTo(1);
        }
        else
        {
            GameObject.FindGameObjectWithTag("PasswordField")
            .GetComponent<Text>().gameObject.transform.parent
            .GetComponent<InputField>().text = "";
            GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(17);
        }
    }
}
