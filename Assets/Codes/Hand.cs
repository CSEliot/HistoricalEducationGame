using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

    private LinkedList<GameObject> Cards;
    public Deck deck;
    public Transform[] HandCardPositions;
    private bool IsAI;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
    
    }

    public void NewGame(){
        Cards = new LinkedList<GameObject>();
    }

    public void DrawCard(){
        while (Cards.Count < 5) {
            Cards.AddLast(deck.Top());
            Cards.Last.Value.transform.SetParent(HandCardPositions[
                Cards.Count - 1]);
            Cards.Last.Value.transform.localPosition = new Vector3(0, 0, 0);
            Cards.Last.Value.transform.localScale = new Vector3(1f, 1f, 1f);
            Cards.Last.Value.GetComponent<Card>().SetNumPos(Cards.Count - 1);
        }
    }

    public void RemoveCard(int CardNumber)
    {
        Go.Do("Card NUmber: " + CardNumber);
        RemoveRcsv(0, CardNumber, Cards.First);
    }

    private void RemoveRcsv(int test, int target, LinkedListNode<GameObject> node)
    {
        Go.Do(""+test+" "+target);
        if (test != target)
        {
            RemoveRcsv(test + 1, target, node.Next);
        }
        else
        {
            node.Value.SetActive(false); //hide the card till new loc. chosen.
            Cards.Remove(node);
        }
        if (test == 0)
        {
            FixHand(0, Cards.First);
        }
    }

    public Transform GetRandomCard()
    {
        //since decks are shuffled, this is technically a random card.
        return Cards.First.Value.transform;
    }

    private void FixHand(int start, LinkedListNode<GameObject> node)
    {
        if (node.Value.transform.parent == null)
        {
            Go.Do("Sa");
        }
        else if (HandCardPositions[start] == null)
        {
            Go.Do("sass");
        }
        node.Value.transform.SetParent(HandCardPositions[start]);
        node.Value.transform.localPosition = new Vector3(0, 0, 0);
        node.Value.transform.localScale = new Vector3(1f, 1f, 1f);
        node.Value.GetComponent<Card>().SetNumPos(start);
        if (node.Next != null)
        {
            FixHand(start + 1, node.Next);
        }    
    }
}
