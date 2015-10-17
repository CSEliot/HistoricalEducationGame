using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Deck : MonoBehaviour {

	private int DeckSize = 30;

	private enum Card{
		Plus1,
		Plus2,
		Plus3,
		Clear,
		Stop,
		Double,
		Sp1,Sp2,Sp3,Sp4,Sp5,
		Sp6,Sp7,Sp8,Sp9,Sp10,
		Sp11,Sp12,Sp13,Sp14,Sp15
	};

	private int[] CardQuantities = new int[]{3,2,3,2,2,3};

	private int[] CardToRemove = new int[]{}; 
	// given the ith number of special cards to use, tells which normal card to replace.

	private Stack <int> deck;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void FillDeck(int DeckLevel){
		deck = new Stack<int> (30);
		for (int x = 0; x < DeckSize; x++) {

		}
	}

	public void Top(){
	}


}
