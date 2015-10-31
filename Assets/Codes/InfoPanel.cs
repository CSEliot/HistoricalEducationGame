using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Text InfoTitle;
    public Image InfoImage;
    public Text InfoDesc;

    private string[] InfoStrings;

    // Use this for initialization
    void Start () {
        AssignInfoStrings();
        transform.gameObject.SetActive(false);
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void SetInfo(string NewTitle, Sprite NewImage, int cardType)
    {
        InfoTitle.text = NewTitle;
        InfoImage.sprite = NewImage;
        //start of first special cards is #6
        Debug.Log("Card Type Set is: " + cardType);
        InfoDesc.text = InfoStrings[cardType-6];
    }

    private void AssignInfoStrings(){
        InfoStrings = new string[]{
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""
        };
    }

    public void CloseInfo()
    {
        gameObject.SetActive(false);
    }
}
