using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class WinPanelCardUnlock : MonoBehaviour {

    public Image CardImage;
    public Text CardText;
    public Text CardTitle;
    public Text WinText;

    private string OGText;

    // Use this for initialization
    void Awake () {
        OGText = WinText.text;
    }

    void OnEnable()
    {
        Debug.Log("NEW CARD UNLOCK DEBUG TEST");
        int newLevel = GameObject.FindGameObjectWithTag("MenuController").
            GetComponent<LevelTracking>().GetLevel();
        string title = GameObject.FindGameObjectWithTag("DeckYours").
            GetComponent<Deck>().TitleList[newLevel + 5];
        CardImage.sprite = GameObject.FindGameObjectWithTag("DeckYours").
            GetComponent<Deck>().CardImages[newLevel + 5];
        CardTitle.text = title;
        CardText.text = GameObject.FindGameObjectWithTag("DeckYours").
            GetComponent<Deck>().FlavorList[newLevel + 5];
        WinText.text = OGText.Replace("<cardname>", title);
    }

    // Update is called once per frame
    void Update () {
    
    }
}
