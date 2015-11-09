using UnityEngine;
using System.Collections;

public class CardChoiceScript : MonoBehaviour {

    // Use this for initialization
    void Start () {
    
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    void OnEnable()
    {
        //if it's AI turn, choose for the AI
        if(!GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>().GetIsPlayerTurn())
        {
            int cardChoice = UnityEngine.Random.Range(1, 3);
            if (cardChoice == 1)
            {
                PlayCard(0);
            }
            else
            {
                PlayCard(5);
            }
        }
    }

    /// <summary>
    /// Creates a new fake "HandPlayed" event to the turn manager
    /// with a -1 hand location so that nothing gets removed for the
    /// new card.
    /// </summary>
    /// <param name="type">for this game, either 1 or 5</param>
    public void PlayCard(int type)
    {
        GameObject.FindGameObjectWithTag("TurnManager").
            GetComponent<TurnManager>().HandPlayed(
                GameObject.FindGameObjectWithTag("DeckYours").
                GetComponent<Deck>().BuildClone(type).transform,
                -1
                );
        gameObject.SetActive(false);
    }
}
