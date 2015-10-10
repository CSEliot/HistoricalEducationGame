using UnityEngine;
using System.Collections;

public class Buttons : MonoBehaviour {

    public float NewSceneNum = 0;

    void Update()
    {
        if (NewSceneNum != 0)
        {
            CallSceneChange(NewSceneNum);
			NewSceneNum = 0;
        }
    }
    public void CallSceneChange(float NewScene)
    {
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
