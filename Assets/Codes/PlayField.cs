using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayField : MonoBehaviour {

    public List<GameObject> field;

    public TurnManager MyManager;

    public float ActivationTime;

    private int[] IsDisabled;
    private bool nextIsDoubled;
    private InfluenceManager MyInfluenceMan;

    // Use this for initialization
    void Start () {
        MyInfluenceMan = GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>();
        nextIsDoubled = false;
        IsDisabled = new int[5]{0,0,0,0,0};
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void NewGame()
    {
        nextIsDoubled = false;
        IsDisabled = new int[5] { 0, 0, 0, 0, 0 };
        Clear(); //from previous games.
    }

    IEnumerator ActivateField()
    {
        Color Light = new Color(2f, 2f, 2f, 2f);
        yield return new WaitForSeconds(ActivationTime);
        for (int pos = 0; pos < 5; pos++ )
        {
            //don't activate if not holding a card,
            // or that position is disabled
            if (field[pos].transform.childCount != 0 &&
                IsDisabled[pos] != 1)
            {
                //Debug.Log("Activating Card: " + pos);
                //Different cards per game, different layout:
                if (field[pos].transform.GetChild(0).
                    GetComponent<Image>() == null)
                {
                    field[pos].transform.GetChild(0).GetChild(1).
                        GetComponent<Image>().CrossFadeColor(Light, 0f, false, true);
                    field[pos].transform.GetChild(0).GetChild(0).
                        GetComponent<Image>().CrossFadeColor(Light, 0f, false, true);
                }
                else
                {
                    field[pos].transform.GetChild(0).
                        GetComponent<Image>().CrossFadeColor(Light, 0f, false, true);
                    field[pos].transform.GetChild(0).GetChild(3).
                        GetComponent<Image>().CrossFadeColor(Light, 0f, false, true);
                }
                field[pos].transform.GetChild(0).
                    GetComponent<Card>().SpecialAbility();
                yield return new WaitForSeconds(ActivationTime);
            }
        }
        MyManager.EndTurn();
    }

    public void Clear()
    {
        //Debug.Log("Clear played");
        foreach (GameObject CardField in field)
        {
            if (CardField.transform.childCount != 0)
            {
                Destroy(CardField.transform.GetChild(0).gameObject);
            }
        }
    }

    //From Hand, To playfield
    public void PlaceCard(Transform newCard, int pos)
    {
        newCard.gameObject.SetActive(true);
        //get rid of any old card there.
        if (field[pos].transform.childCount != 0)
        { 
            Debug.Log("Destroyin the theingd: " + field[pos].transform.childCount);
            Destroy(field[pos].transform.GetChild(0).gameObject);
        }
        newCard.transform.SetParent(field[pos].transform, false);
        field[pos].transform.GetChild(0).localPosition =
            new Vector3(0f, 0f, 0f);
        field[pos].transform.GetChild(0).localScale =
            new Vector3(1f, 1f, 1f);
        newCard.GetComponent<Card>().SetNumPos(pos);
        field[pos].transform.GetChild(0).localPosition =
            new Vector3(0f, 0f, 0f);
        Destroy(field[pos].transform.GetChild(0).gameObject.GetComponent<Button>());
        //field[pos].transform.GetChild(0).gameObject.AddComponent<Button>().getc
    }



    public void ActivateCardsOnField()
    {
        //darken all cards, in coroutine re-light on use.
        foreach (GameObject CardField in field)
        {
            Color Dark = new Color(0.4f, 0.4f, 0.4f, 0.4f);
            if (CardField.transform.childCount != 0)
            {
                //Debug.Log("Darkening Card");
                //Different cards per game, different layout:
                if (CardField.transform.GetChild(0).
                    GetComponent<Image>() == null)
                {
                    CardField.transform.GetChild(0).GetChild(1).
                    GetComponent<Image>().CrossFadeColor(Dark, ActivationTime, false, true);
                    CardField.transform.GetChild(0).GetChild(0).
                        GetComponent<Image>().CrossFadeColor(Dark, ActivationTime, false, true);
                }
                else
                {
                    CardField.transform.GetChild(0).
                        GetComponent<Image>().CrossFadeColor(Dark, ActivationTime, false, true);
                    CardField.transform.GetChild(0).GetChild(3).
                        GetComponent<Image>().CrossFadeColor(Dark, ActivationTime, false, true);
                }
            }
        }
        StartCoroutine(ActivateField());
    }

    public void Stop(int stopPos)
    {
        IsDisabled[stopPos] = 1;
    }

    public void UnStop(int stopPos)
    {
        Debug.Log("Unstopping Pos: " + stopPos + " of " + gameObject.name);
        IsDisabled[stopPos] = 0;
    }

}
