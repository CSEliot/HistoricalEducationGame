using UnityEngine;
using System.Collections;

public class MaxLevel : MonoBehaviour {

    public LevelTracking tracker;
    int totalPresses = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void CountPresses()
    {
        Debug.Log("Pressed Cheat button!");
        totalPresses++;
        if (totalPresses > 11)
        {
            tracker.MaxLevelCheat();
            totalPresses = 0;
        }
    }
}
