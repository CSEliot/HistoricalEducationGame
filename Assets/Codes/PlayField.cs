using UnityEngine;
using System.Collections;


public class PlayField : MonoBehaviour {

    public GameObject[] field;
    private bool turnEnded;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void NewGame()
    {
        field = new GameObject[5];
        turnEnded = false;
    }

    IEnumerator ActivateField()
    {
        foreach (GameObject Card in field)
        {
            Card.GetComponent<Card>().SpecialAbility();
            yield return new WaitForSeconds(0.3f);
        }
        turnEnded = true;
    }

    public void Clear()
    {
        foreach (GameObject Card in field)
        {
            Destroy(Card);
        }
    }

    public void PlaceCard()
    {
        
    }

    public bool FinishedActivating()
    {
        return turnEnded;
    }


}
