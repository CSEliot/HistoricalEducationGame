using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfluenceManager : MonoBehaviour {

    private int influenceCount;
    public GameObject YourBar;
    public GameObject InfluenceText;
    private int AITurn;  //influence manager should know when to
                        //increase or decrease influence.
                        //-1 means AITurn
    private int DoubleCount;


    // Use this for initialization
    void Start () {
        
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void NewGame()
    {
        AITurn = 1;
        influenceCount = 15;
        DoubleCount = 1;
        YourBar.GetComponent<Image>().fillAmount = 0.5f;
        InfluenceText.GetComponent<Text>().text = "15/30";
    }

    public void DecreaseInfluence(int amount)
    {
        amount *= AITurn;
        amount *= DoubleCount; //applying modification from any double cards.
        //Once double's been used, reset it.
        DoubleCount = 1;
        //floor influence at 0
        influenceCount = ((influenceCount-amount) < 0 )? 0 : 
            influenceCount - amount;
        YourBar.GetComponent<Image>().fillAmount = influenceCount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + influenceCount+ "/30";
    }

    public void IncreaseInfluence(int amount)
    {
        amount *= AITurn; //AITurn = -1
        amount *= DoubleCount; //applying modification from any double cards.
        //Once double's been used, reset it.
        DoubleCount = 1;
        //cap influence at 30
        Debug.Log("Increasing by amount: " + amount);
        influenceCount = ((influenceCount + amount) > 30) ? 30 :
            influenceCount + amount;
        YourBar.GetComponent<Image>().fillAmount = influenceCount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + influenceCount + "/30";
    }

    public void SetInfluence(int amount)
    {
        influenceCount = amount;
        YourBar.GetComponent<Image>().fillAmount = amount / 30f;
        InfluenceText.GetComponent<Text>().text = "" + amount + "/30";
    }

    public int GetWinStatus()
    {
        if (influenceCount >= 30)
        {
            return 1;
        }
        else if (influenceCount <= 0)
        {
            return -1;
        }else{
            return 0;
        }
    }

    public void TurnChange()
    {
        AITurn = AITurn * -1;
        DoubleCount = 1;
    }

    public void DoubleNext()
    {
        DoubleCount = 2;
    }


    public void TripleNext()
    {
        DoubleCount = 3; //bad variable name, sue me.
    }

    public void CancelDouble()
    {
        DoubleCount = 1;
    }
}
