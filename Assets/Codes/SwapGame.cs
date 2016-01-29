using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SwapGame : MonoBehaviour {

    private int totalPresses;
    private int otherSceneNum;

	// Use this for initialization
	void Start () {
        otherSceneNum = SceneManager.GetActiveScene().buildIndex == 1 ? 0 : 1;
	    totalPresses = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (totalPresses > 20)
        {
            totalPresses = 0;
            GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(1);
            SceneManager.LoadScene(otherSceneNum);
        }
	}

    public void ButtonPressCounting()
    {
        Debug.Log("Button Pressed for GameSwap");
        totalPresses++;
    }
}
