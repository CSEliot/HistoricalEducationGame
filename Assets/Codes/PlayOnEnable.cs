using UnityEngine;
using System.Collections;

public class PlayOnEnable : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        transform.GetComponent<AudioSource>().Play();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
