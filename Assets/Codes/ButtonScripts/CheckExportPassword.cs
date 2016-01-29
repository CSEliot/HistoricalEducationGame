using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CheckExportPassword : MonoBehaviour {

    private string pass = "hello";
    public AllMenuNav MenuNavScript;
    public DataTracking DataTracking;

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
            gameObject.transform.parent
               .GetComponent<InputField>().text = "";
            Debug.Log("Exporting Data");
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<DataTracking>().SaveData(false);
            GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(13);
            MenuNavScript.ChangeSceneTo(0);
        }
        else
        {
            gameObject.transform.parent
               .GetComponent<InputField>().text = "";
            GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(17);
        }
    }
}
