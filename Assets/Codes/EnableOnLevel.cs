using UnityEngine;
using System.Collections;

public class EnableOnLevel : MonoBehaviour {

    public GameObject EnableObj;

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        Debug.Log("Thing enabled");
        if(GameObject.FindGameObjectWithTag("MenuController").
            GetComponent<LevelTracking>().GetLevel() != 0)
        {
            Debug.Log("Other thing enabled");
            EnableObj.SetActive(true);
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
