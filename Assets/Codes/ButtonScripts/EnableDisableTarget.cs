using UnityEngine;
using System.Collections;

public class EnableDisableTarget : MonoBehaviour {

    public GameObject Target;

    private bool isEnabled;

	// Use this for initialization
	void Start () {
        if (Target.activeSelf)
        {
            isEnabled = true;
        }
        else
        {
            isEnabled = false;
        }
        if (Application.loadedLevelName.Contains("Pop"))
        {
             
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleEnabled()
    {
        Debug.Log("I ALSO MAKE BAD TIME");
        isEnabled = !isEnabled;
        Target.SetActive(isEnabled);
        if (isEnabled)
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<MusicManager>().SetMusic(4);
        else
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<MusicManager>().Rewind();
    }
}
