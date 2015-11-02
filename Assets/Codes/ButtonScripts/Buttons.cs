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
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<AllMenuNav>().ChangeSceneTo((int)NewScene);
        }
    }

    public void QuitGameActually()
    {
        Application.Quit();
    }
}
