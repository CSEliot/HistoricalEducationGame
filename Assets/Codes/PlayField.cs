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
    {//nothing for now.
    }

    IEnumerator ActivateField()
    {
        for (int pos = 0; pos < 5; pos++ )
        {
            //don't activate if not holding a card,
            // or that position is disabled
            if (field[pos].transform.childCount != 0 &&
                IsDisabled[pos] != 1)
            {
                Go.Do("Activating Card: " + pos);
                field[pos].transform.GetChild(0).
                    GetComponent<Image>().color =
                    new Color(255f, 255f, 255f, 255f);
                field[pos].transform.GetChild(0).
                    GetChild(3).GetComponent<Image>().color =
                    new Color(255f, 255f, 255f, 255f);
                field[pos].transform.GetChild(0).
                    GetComponent<Card>().SpecialAbility();
                yield return new WaitForSeconds(ActivationTime);
            }
        }
        MyManager.EndTurn();
    }

    public void Clear()
    {
        Go.Do("Clear played");
        foreach (GameObject CardField in field)
        {
            if (CardField.transform.childCount != 0)
            {
                Destroy(CardField.transform.GetChild(0).gameObject);
            }
        }
    }

    public void PlaceCard(Transform newCard, int pos)
    {
        newCard.gameObject.SetActive(true);
        //get rid of any old card there.
        if (field[pos].transform.childCount != 0)
        {
            Go.Do("Destroyin the theingd: " + field[pos].transform.childCount);
            Destroy(field[pos].transform.GetChild(0).gameObject);
        }
        newCard.transform.SetParent(field[pos].transform);
        field[pos].transform.GetChild(0).localScale = 
            new Vector3(1f, 1f, 1f);
        field[pos].transform.GetChild(0).localPosition =
            new Vector3(0f, 0f, 0f);
        newCard.GetComponent<Card>().SetNumPos(pos);
    }

    public void ActivateCardsOnField()
    {
        //darken all cards, in coroutine re-light on use.
        foreach (GameObject CardField in field)
        {
            if (CardField.transform.childCount != 0)
            {
                CardField.transform.GetChild(0).
                    GetComponent<Image>().color =
                    new Color(255f, 255f, 255f, 69f);
                CardField.transform.GetChild(0).
                    GetChild(3).GetComponent<Image>().color =
                    new Color(255f, 255f, 255f, 69f);
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
        IsDisabled[stopPos] = 0;
    }

}
