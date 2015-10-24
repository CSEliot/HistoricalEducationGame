using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

    private LinkedList<GameObject> Cards;
    public Deck deck;
    public Transform[] HandCardPositions;

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
            Cards.Last.Value.transform.po = HandCardPositions[
                Cards.Count-1];
        }
    }

    public void PlayCard(int ChosenCardNum){
        //Animation Calls
        //Put card in Field list. (C# defaults to PbV)
        //Remove card from Hand list.
    }
}
