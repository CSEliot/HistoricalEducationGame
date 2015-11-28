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
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ToggleEnabled()
    {
        isEnabled = !isEnabled;
        Target.SetActive(isEnabled);
        if (isEnabled)
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<MusicManager>().SetMusic(2);
        else
            GameObject.FindGameObjectWithTag("MenuController").
                GetComponent<MusicManager>().Rewind();
    }
}
