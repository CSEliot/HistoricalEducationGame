using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

	private List<Card> Cards;
	private int HandSize;
	private Deck deck;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NewGame(){
		Cards = new List<Card> (5);
	}

	public void DrawCard(Card NewCard){

		Cards.Add (NewCard);
	}

	public void PlayCard(int ChosenCardNum){
		//Animation Calls
		//Put card in Field list. (C# defaults to PbV)
		//Remove card from Hand list.
	}
}
