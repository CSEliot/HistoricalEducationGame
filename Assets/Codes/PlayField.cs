using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class PlayField : MonoBehaviour {

    public List<GameObject> field;

    public TurnManager MyManager;

    public float ActivationTime;

    private int[] IsDisabled;
    private bool nextIsDoubled;
    private InfluenceManager MyInfluenceMan;
    private bool IsAI;
    private int[] UnstoppableCardTypes = new int[] {11};
    private bool card15Activated; //extreme edge case

    // Use this for initialization
    void Start () {
        card15Activated = false;
        MyInfluenceMan = GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>();
        nextIsDoubled = false;
        IsDisabled = new int[5]{0,0,0,0,0};
        IsAI = gameObject.name.Contains("AI")? true : false;
    }
    
    // Update is called once per frame
    void Update () {
        
    }

    public void NewGame()
    {
        card15Activated = false;
        nextIsDoubled = false;
        IsDisabled = new int[5] { 0, 0, 0, 0, 0 };
        Clear(); //from previous games.
    }

    IEnumerator ActivateField()
    {
        Color Light = new Color(2f, 2f, 2f, 2f);
        yield return new WaitForSeconds(ActivationTime);

        //certain cards are immune to stop. For now
        ////it's only 1 card.
        //bool isUnstoppable = field[pos].transform.GetChild(0).
        //            GetComponent< 

        for (int pos = 0; pos < 5; pos++ )
        {
            //don't activate if not holding a card,
            // or that position is disabled
            if (field[pos].transform.childCount != 0 &&
                (IsDisabled[pos] != 1))
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
                    field[pos].transform.GetChild(0).GetChild(2).
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



    public IEnumerator ActivateCardsOnField()
    {
        yield return new WaitForSeconds(0.2f);
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
                    CardField.transform.GetChild(0).GetChild(2).
                        GetComponent<Image>().CrossFadeColor(Dark, ActivationTime, false, true);
                }
            }
        }
        StartCoroutine(ActivateField());
    }

    public void Stop(int stopPos)
    {
        //There's one card that can't be stopped. If it's in play,
        //we have to do a check
        //problem: cards are not stopped, but the location is.
        //         and a location can be stopped before anything is played on
        //          it. . . . . 
        //Solution: Have the stop function test to see if the card at the
        //location is a stoppable card.
        //Edge Case: If a card is stoppable, the location becomes stopped, then
        //always unstop the location when the stopped card is removed and 
        //only stop locations with stoppable cards in them.
        bool containsCard = field[stopPos].transform.childCount == 0? false:true;
        bool isUnstoppable = true;
        if (containsCard)
        {
            int cardType = field[stopPos].transform.GetChild(0).GetComponent<Card>().
                GetCardType();
            isUnstoppable = Array.
                IndexOf(UnstoppableCardTypes, cardType) == -1? false:true;
        }

        if (containsCard && !isUnstoppable)
        {
            IsDisabled[stopPos] = 1;
        }
    }

    public void ConvertAllCardsTo(int cardType)
    {
        int totalSpaces = 5;
        for (int i = 0; i < totalSpaces; i++)
        {
            //this method only converts existing cards
            if (field[i].transform.childCount != 0)
            {
                if (IsAI)
                {
                    PlaceCard(
                        GameObject.FindGameObjectWithTag("DeckAI").
                        GetComponent<Deck>().BuildClone(cardType).transform,
                        i
                    );
                }
                else
                {
                    PlaceCard(
                        GameObject.FindGameObjectWithTag("DeckYours").
                        GetComponent<Deck>().BuildClone(cardType).transform,
                        i
                    );
                }
                GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(5);
            }
        }
    }

    
    public void DisableAll()
    {
        IsDisabled = new int[] { 1, 1, 1, 1, 1 };
    }

    public void ConvertWholeFieldTo(int cardType)
    {
        int totalSpaces = 5;
        for (int i = 0; i < totalSpaces; i++)
        {
            if (IsAI)
            {
                PlaceCard(
                    GameObject.FindGameObjectWithTag("DeckAI").
                    GetComponent<Deck>().BuildClone(cardType).transform,
                    i
                );
            }
            else
            {
                PlaceCard(
                    GameObject.FindGameObjectWithTag("DeckYours").
                    GetComponent<Deck>().BuildClone(cardType).transform,
                    i
                );
            }
            GameObject.FindGameObjectWithTag("SFXController").
            GetComponent<SoundEffectManager>().PlaySound(5);
        }
    }

    public void UnStop(int stopPos)
    {
        Debug.Log("Unstopping Pos: " + stopPos + " of " + gameObject.name);
        IsDisabled[stopPos] = 0;
    }

    public bool GetIf15Activated()
    {
        //set card15activated afterwards
        StartCoroutine(SetPostReturn());
        return card15Activated;
    }
    ///Helper for GetIf15Activated
    private IEnumerator SetPostReturn()
    {
        yield return new WaitForSeconds(0.5f);
        card15Activated = true;
    }
}
