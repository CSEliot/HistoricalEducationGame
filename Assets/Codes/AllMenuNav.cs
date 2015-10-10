using UnityEngine;
using System.Collections;

public class AllMenuNav : MonoBehaviour {

	public int CurrentCanvasNum;

	public GameObject[] Canvases;

	// Use this for initialization
	void Start () {
	    //disables all canvases in case I was messing around in one.
        //then enables the one I want to test in play mode via "CurrentCanvasNum"
        //game will be exported at Canvas 0.
        foreach (GameObject Canvas in Canvases)
        {
            Canvas.SetActive(false);
        }
        if (!Application.isEditor) { CurrentCanvasNum = 0; }
        Canvases[CurrentCanvasNum].SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ChangeSceneTo(int MenuInt)
    {
        if (MenuInt == 5)
        {
            Canvases[MenuInt].SetActive(true);
        }
        else {
            Canvases[CurrentCanvasNum].SetActive(false);
            Canvases[MenuInt].SetActive(true);
            CurrentCanvasNum = MenuInt;
        }
    }
}
