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
    public GameObject KnowPrefabTest;
    public Sprite[] CardImages;

    
    public string[] TitleList;
    public string[] FlavorList;

    private int myStageNum;
    private int myPlayerLevel;

    private enum CardType{
        Plus1, //0 - Min: 3
        Plus2, //1 - Min: 2
        Plus3, //2 - Min: 3
        Clear, //3 - Min: 2
        Stop,  //4 - Min: 2
        Double,//5 - Min: 3
        Sp1,Sp2,Sp3,Sp4,Sp5, //special = sp#+6
        Sp6,Sp7,Sp8,Sp9,Sp10,
        Sp11,Sp12,Sp13,Sp14,Sp15
    };

    private int[] CardQuantities = new int[] { 3, 2, 3, 2, 2, 3 };

    // given the ith number of special cards to use, tells which 
    // normal card to replace.
    private int[] CardToRemove = new int[]{4,0,5,5,1,2,3,0,4,1,2,5,3,2,0};


    private int[] IsEventList = new int[] { 0, 0, 0, 1, 0, 0, 0, 1, 1, 1, 
                                            1, 0, 1, 1, 0, 0, 0, 1, 1, 0, 1 };

    private bool[] InfoDisplayed = new bool[] { false, false, false, false, 
        false, false,false,false,false,false,false,false,false,false,false};
        
    private LinkedList<int> deck;

    // Use this for initialization
    void Start () {
        if (gameObject.name.Contains("AI"))
        {
            Debug.Log("Is Deck AI? " + gameObject.name);
            IsAI = true;
        }
        else
        {
            IsAI = false;
        }
    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("k"))
        {
            PrintLinkedList<int>(deck.First); ;
        }
    }

    public void NewGame(int StageNum, int PlayerLevel)
    {
        myStageNum = StageNum;
        myPlayerLevel = PlayerLevel;
        FillDeck(StageNum, PlayerLevel);
    }

    private void FillDeck(int DeckLevel, int PlayerLevel){
        myPlayerLevel = PlayerLevel;
        myStageNum = DeckLevel;
        //the highest number special card to replace a regular one.
        int specialReplaceNum = IsAI ? DeckLevel+1 : PlayerLevel; 
        //the indice of the special card replacement number
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
                    deck.AddLast(x);
                }
            }
        }


        Debug.Log("Deckstring is: " + printDeck(deck.First));
        FindAndReplace(specialReplaceNum);

        Debug.Log("Deckstring is: " + printDeck(deck.First));
        //Debug.Log("Print out LinkedList");
        //PrintLinkedList<int>(deck.First);
        int totalShuffles = 7;
        //SEVEN SHUFFLES CUZ MATH
        UnityEngine.Random.seed = (int)Time.time;
        for (int x = 0; x < totalShuffles; x++)
        {
            ShuffleDeck();
        }
        GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(16);

        PutLastUnlockedAtTop(specialReplaceNum);
    }

    private void ReFillDeck()
    {
        //the highest number special card to replace a regular one.
        int specialReplaceNum = IsAI ? myStageNum + 1 : myPlayerLevel;
        //the indice of the special card replacement number
        int BasicTypeCount = 6;
        for (int twice = 0; twice < 2; twice++)
        {
            //the card type to add
            for (int x = 0; x < BasicTypeCount; x++)
            {
                //how many of that type to add
                for (int i = 0; i < CardQuantities[x]; i++)
                {
                    deck.AddLast(x);
                }
            }
        }

        FindAndReplace(specialReplaceNum);
        //Debug.Log("Print out LinkedList");
        //PrintLinkedList<int>(deck.First);
        int totalShuffles = 7;
        //SEVEN SHUFFLES CUZ MATH
        UnityEngine.Random.seed = (int)Time.time;
        for (int x = 0; x < totalShuffles; x++)
        {
            ShuffleDeck();
        }
        PutLastUnlockedAtTop(specialReplaceNum);
    }
    //if (x == CardToRemove[{replaceNum] &&
    //                    replaceNum < SpecialReplaceNum)
    //                {
    //                    deck.AddLast(replaceNum);
    //                    replaceNum++;
    //                }
    //                else
    //                {
    //                    //for each type, add i amount of card x
    //                    deck.AddLast(x);
    //                }

    private void FindAndReplace(int specialReplaceNum)
    {
        //cieling @ 14
        specialReplaceNum = (specialReplaceNum > 15) ? 15 : specialReplaceNum;
        for (int x = 0; x < specialReplaceNum; x++)
        {
            //6 is the first number for the special values
            deck.Find(CardToRemove[x]).Value = x + 6;
            Debug.Log("Replacing " + deck.Find(CardToRemove[x]).Value + "with"
                + (x + 6));
        }
    }

    private void PutLastUnlockedAtTop(int specialReplaceNum)
    {
        specialReplaceNum = (specialReplaceNum > 15) ? 15 : specialReplaceNum;
        int cardAtTop = deck.Last.Value;
        Debug.Log("IsAI: "+IsAI+" Replacing number: " + specialReplaceNum);
        deck.Find(specialReplaceNum + 5).Value = cardAtTop;
        deck.Last.Value = specialReplaceNum + 5;//offset for special value
        //5 instead of 6 because specialReplaceNum starts at 0 not 1
        
    }

    private string printDeck(LinkedListNode<int> start)
    {
        string deckString = "";
        if(start.Next == null)
            return "";

        deckString += printDeck(start.Next);
        return deckString+","+start.Value;
    }

    public GameObject Top(){
        if (deck.Count <= 2)
        {
            ReFillDeck();
        }
        int cardNum = deck.Last.Value;
        GameObject tempCard = Instantiate(CardPrefab) as GameObject;
        //Debug.Log("Instantiating Card: " + TitleList[cardNum]);
        //create a card based on the number at the top of the deck.
        tempCard.GetComponent<Card>().AssignData(CardImages[cardNum], 
            TitleList[cardNum],FlavorList[cardNum],(cardNum < 15)? false : true,
            Convert.ToBoolean(IsEventList[cardNum]), cardNum, IsAI);

        deck.RemoveLast ();
        //Every time a card is removed (drawn), if it's a special card
        // display it if it hasn't been displayed before, else,
        // don't display
        if (!IsAI)
        {
            //if it's a special card (#>=6) and it hasn't been displayed before
            if (cardNum > 5 && !InfoDisplayed[cardNum - 6])
            {
                Debug.Log("Displaying Info for cardNum:" + cardNum);
                InfoDisplayed[cardNum - 6] = true;
                ActivateInfo(cardNum);
            }
        }
        return tempCard;
    }

    /// <summary>
    /// Given card number, it'll build a card to be reused elsewhere.
    /// This is intended to be used with cards that spawn more cards
    /// or turn cards into other cards.
    /// </summary>
    /// <param name="cardNum"></param>
    /// <returns>New Card with stats for cardNum</returns>
    public GameObject BuildClone(int cardNum)
    {
        GameObject tempCard = Instantiate(CardPrefab) as GameObject;
        //Debug.Log("Instantiating Card: " + TitleList[cardNum]);
        //create a card based on the number at the top of the deck.
        tempCard.GetComponent<Card>().AssignData(CardImages[cardNum],
            TitleList[cardNum], FlavorList[cardNum], (cardNum < 15) ? false : true,
            Convert.ToBoolean(IsEventList[cardNum]), cardNum, IsAI);
        return tempCard;
    }

    private void ShuffleDeck()
    {
        int randNum;
        for (LinkedListNode<int> n = deck.First; n != null; n = n.Next)
        {
            int num = n.Value;
            //swap with top, or bottom.
            
            randNum = UnityEngine.Random.Range(1, 3);
            //Debug.Log("Random Number is: " + randNum);
            if (randNum == 1)
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

    private void ActivateInfo(int specialCard)
    {
        GameObject tempthing = Instantiate(KnowPrefabTest) as GameObject; 
        tempthing.GetComponent<InfoPanel>().
            SetInfo(TitleList[specialCard], CardImages[specialCard],
            specialCard);
    }

    private void PrintLinkedList<T>(LinkedListNode<T> Current)
    {
        Debug.Log(Current.Value);
        if (Current.Next != null)
        {
            PrintLinkedList<T>(Current.Next);
        }
    }
}
