using UnityEngine;
using System.Collections;

public class ChapterLearnButton : MonoBehaviour {

    public int ChapterNum;
    public AllChapterButtons ButtonObjectScript;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ActivateButton()
    {
        int totalChapters = 7;
        for (int i = 0; i < totalChapters; i++){
            //Disable all first.
            ButtonObjectScript.AllChapters[i].SetActive(false);
        }
        ButtonObjectScript.AllChapters[ChapterNum-1].SetActive(true);
    }
}
