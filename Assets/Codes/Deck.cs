using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Deck : MonoBehaviour {

    //DECK WILL BUILD CARDS AND CONTAIN IMAGES CUZ REASONS

	private int DeckSize = 30;
	private bool IsAI;
    public GameObject CardPrefab;

    public Sprite[] CardImages;


    public string[] TitleList;
    public string[] FlavorList;

	private enum CardType{
		Plus1, //0 - Min: 3
		Plus2, //1 - Min: 2
		Plus3, //2 - Min: 3
		Clear, //3 - Min: 2
		Stop,  //4 - Min: 2
		Double,//5 - Min: 3
		Sp1,Sp2,Sp3,Sp4,Sp5, //special = sp#+5
		Sp6,Sp7,Sp8,Sp9,Sp10,
		Sp11,Sp12,Sp13,Sp14,Sp15
	};

	private int[] CardQuantities = new int[] { 3, 2, 3, 2, 2, 3 };

	// given the ith number of special cards to use, tells which 
    // normal card to replace.
	private int[] CardToRemove = new int[]{3,3,1,2,5,4,1,5,0,2,0,2,0,5,4};


    private int[] IsEventList = new int[] { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 
                                            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
	private LinkedList<int> deck;

	// Use this for initialization
	void Start () {
		if (gameObject.name.Contains("AI"))
		{
			IsAI = true;
		}
		else
		{
			IsAI = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NewGame(int StageNum)
	{
		FillDeck(StageNum);
	}

	private void FillDeck(int DeckLevel){
		int BasicTypeCount = 6;
		deck = new LinkedList<int>();
		// adding basics
		//repeat 
		for (int twice = 0; twice < 2; twice++)
		{
            //the card type to add
			for (int x = 0; x < BasicTypeCount; x++)
			{   
                //how many of that type to add
				for (int i = 0; i < CardQuantities[x]; i++)
				{
					//for each type, add i amount of card x
					deck.AddLast(x);
				}
			}
		}
        //SEVEN SHUFFLES CUZ MATH
        ShuffleDeck();
        ShuffleDeck();
        ShuffleDeck();
        ShuffleDeck();
        ShuffleDeck();
        ShuffleDeck();
        ShuffleDeck();
    }

	public GameObject Top(){
        int cardNum = deck.Last.Value;
        GameObject tempCard = Instantiate(CardPrefab) as GameObject;
        //create a card based on the number at the top of the deck.
		tempCard.GetComponent<Card>().AssignData(CardImages[cardNum], 
            TitleList[cardNum],FlavorList[cardNum],(cardNum < 15)? false : true,
            Convert.ToBoolean(IsEventList[cardNum]), cardNum, IsAI);

		deck.RemoveLast ();
        return tempCard;
	}

    private void ShuffleDeck()
    {
        System.Random rand = new System.Random();

        for (LinkedListNode<int> n = deck.First; n != null; n = n.Next)
        {
            int num = n.Value;
            //swap with top, or bottom.
            if (rand.Next(0, 2) == 1)
            {
                n.Value = deck.Last.Value;
                deck.Last.Value = num;
            }
            else
            {
                n.Value = deck.First.Value;
                deck.First.Value = num;
            }
        }
    }

}
