using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfluenceManager : MonoBehaviour {

    private int influenceCount;
    public GameObject YourBar;
    public GameObject InfluenceText;
    public int AITurn;  //influence manager should know when to
                        //increase or decrease influence.
                        //-1 means AITurn

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void NewGame()
    {
        influenceCount = 15;
        YourBar.GetComponent<Image>().fillAmount = 0.5f;
        InfluenceText.GetComponent<Text>().text = "15/30";
    }

    public void DecreaseInfluence(int amount)
    {
        amount *= AITurn;
        influenceCount -= amount;
        YourBar.GetComponent<Image>().fillAmount = amount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + amount + "/30";
    }

    public void IncreaseInfluence(int amount)
    {
        amount *= AITurn;
        influenceCount += amount;
        YourBar.GetComponent<Image>().fillAmount = amount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + amount + "/30";
    }

    public void SetInfluence(int amount)
    {
        influenceCount = amount;
        YourBar.GetComponent<Image>().fillAmount = amount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + amount + "/30";
    }
}
