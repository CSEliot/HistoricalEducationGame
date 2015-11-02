using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour {

    public Text InfoTitle;
    public Image InfoImage;
    public Text InfoDesc;
    
    private RectTransform MyRect; 
    private string[] InfoStrings;

    // Use this for initialization
    void Awake () {
        AssignInfoStrings();
        MyRect = GetComponent<RectTransform>();
        MyRect.SetParent(GameObject.FindGameObjectWithTag("InfoPanelLoc")
            .transform, false);
        //transform.SetParent(GameObject.FindGameObjectWithTag("TurnManager")
        //    .transform);
        
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
        Debug.Log("Info Strings Assigned");
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
        Destroy(gameObject);
    }
}
