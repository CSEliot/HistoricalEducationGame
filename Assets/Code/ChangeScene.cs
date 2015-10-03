using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public float CurrentCanvasNum;

	public GameObject[] Canvases;

	private bool NumChanged;
	private float OldCanvasNum;

	// Use this for initialization
	void Start () {
		CurrentCanvasNum = 0;
		OldCanvasNum = 0;
		NumChanged = false;
        //Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void ChangeSceneAction()
    {
        
    }
}
