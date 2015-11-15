using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckExportPassword : MonoBehaviour {

    private string pass = "hello";


    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void TestPass()
    {
        string enteredPass = gameObject.GetComponent<Text>().text;
        Debug.Log("CSV Export Pass: " + enteredPass);
        if (enteredPass == pass)
        {
            Debug.Log("Exporting Data");
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<DataTracking>().SaveData();
            GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(13);
        }
        else
        {
            GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(17);
        }
    }
}
