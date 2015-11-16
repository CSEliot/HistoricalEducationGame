using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WinPanelCardUnlock : MonoBehaviour {

    public Image CardImage;
    public Text CardText;
    public Text CardTitle;
    public Text WinText;

	// Use this for initialization
	void Start () {
	
	}

    void OnEnable()
    {
        Debug.Log("NEW CARD UNLOCK DEBUG TEST");
        int newLevel = GameObject.FindGameObjectWithTag("MenuController").
            GetComponent<LevelTracking>().GetLevel();
        string title = GameObject.FindGameObjectWithTag("DeckYours").
            GetComponent<Deck>().TitleList[newLevel + 6];
        CardImage.sprite = GameObject.FindGameObjectWithTag("DeckYours").
            GetComponent<Deck>().CardImages[newLevel + 6];
        CardTitle.text = title;
        CardText.text = GameObject.FindGameObjectWithTag("DeckYours").
            GetComponent<Deck>().FlavorList[newLevel + 6];
        WinText.text = WinText.text.Replace("<cardname>", title);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
