using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

    public float NewSceneNum; //animation-based button detection
    
    public void Start()
    {
        NewSceneNum = 0;
    }

    void Update()
    {
        if (NewSceneNum != 0)
        {
            //Debug.Log("Not Zero!");
            CallSceneChange(NewSceneNum);
        }
    }

    public void CallSceneChange(float NewScene)
    {
        NewSceneNum = 0;
        if (NewScene == -1)
        {
            QuitGameActually();
        }
        else
        {
            Debug.Log("New Scene is: " + (int)NewScene);
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<AllMenuNav>().ChangeSceneTo((int)NewScene);
        }
    }

    public void QuitGameActually()
    {
        Debug.Log("Quitting Game . . .");
        Application.Quit();
    }
}
