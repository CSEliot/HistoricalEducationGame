using UnityEngine;
using System.Collections;
using System;
public class TurnManager : MonoBehaviour {

	public Hand PlayerHand;
	public Hand AIHand;
	public Deck PlayerDeck;
	public Deck AIDeck;
	public PlayField PlayerField;
	public PlayField AIField;
	public LevelTracking LevelTracker;
	public InfluenceManager InfluenceManager;
    

	private bool IsPlayerTurn;
    private int CurrentLevel;
    private Transform SuspendedCard;

	private enum Turn{
		Launching,
		FillingHand,
		ChoosingCard,
		ActivatingAbilities,
		SwitchingTurn,
		Waiting,
        ChoosingFieldPos
	};

	private Turn TurnState; 
	// Use this for initialization
	void Start () {
        Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
	}
	
	// Update is called once per frame
	void Update () {
		if(TurnState == Turn.FillingHand){
            //StartCoroutine("PlayerDrawCard");
            PlayerHand.DrawCard();
            AIHand.DrawCard();
			TurnState = Turn.ChoosingCard;
            Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
		}
		if (TurnState == Turn.Launching) {
            AIDeck.NewGame(CurrentLevel);
            PlayerDeck.NewGame(CurrentLevel);
            AIHand.NewGame();
            PlayerHand.NewGame();
            AIField.NewGame();
            PlayerField.NewGame();
            InfluenceManager.NewGame();
			//play teacher lady info on gameplay
			//wait for her to finish

            TurnState = Turn.FillingHand;
            Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
		}
        if (TurnState == Turn.ActivatingAbilities)
        {
            if (IsPlayerTurn)
            {
                PlayerField.ActivateCardsOnField();
            }
            else
            {
                AIField.ActivateCardsOnField();
            }
            TurnState = Turn.Waiting;
            Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
        }
        if (TurnState == Turn.SwitchingTurn)
        {
            InfluenceManager temp = GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>();
            int winCon = temp.GetWinStatus();
            if (winCon == 0)
            {
                //influence manager has to flip adding or subtracting points.
                Go.Do("Switching FROM player? " + IsPlayerTurn);
                temp.TurnChange();
                IsPlayerTurn = !IsPlayerTurn;
                TurnState = Turn.FillingHand;
                Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
                //no winner yet
            }
            else if (winCon == 1) 
            {
                //PLAYER WON
            }
            else if (winCon == -1)
            {
                //AI WON D:
            }
        }
        if (TurnState == Turn.ChoosingCard && !IsPlayerTurn)
        {
            //AI can't press buttons, so we press for it.
            HandPlayed(AIHand.GetRandomCard(), 0);
        }
        if (TurnState == Turn.ChoosingFieldPos && !IsPlayerTurn)
        {
            //AI can't press buttons, so we press for it.
            CardSpotChosen(UnityEngine.Random.Range(0, 5));
        }
	}

    IEnumerator CardPlayed(){
        yield return new WaitForSeconds(2f);
        //wait for abilities to go off.
        TurnState = Turn.SwitchingTurn; Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
    }

	public void EndTurn(){
        TurnState = Turn.SwitchingTurn;
        Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
	}

	public void Launch(int StageNum)
	{
		IsPlayerTurn = true;
        CurrentLevel = StageNum;
		TurnState = Turn.Launching;
        Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
	}

    public void HandPlayed(Transform theChosenOne, int HandNumber)
    {
        if (TurnState == Turn.ChoosingCard)
        {
            //We save a reference to the picked card to place on the field.
            SuspendedCard = theChosenOne;
            bool tempIsEvent = SuspendedCard.GetComponent<Card>().GetIsEvent();
            if (IsPlayerTurn)
            {
                Go.Do("HandNumber played by Player: " + HandNumber);
                //handnumber tells us which one to remove from 
                // hand (not delete)
                PlayerHand.RemoveCard(HandNumber);
                if (!tempIsEvent)
                {
                    TurnState = Turn.ChoosingFieldPos;
                    Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
                    
                    //StartCoroutine(WaitingForField());
                }
                else
                {
                    Go.Do("Event Played");
                    //if event card, play from hand and not onto field.
                    SuspendedCard.GetComponent<Card>().SpecialAbility();
                    SuspendedCard = null;
                    Destroy(theChosenOne.gameObject);
                    TurnState = Turn.ChoosingFieldPos;
                }
            }
            else
            {
                AIHand.RemoveCard(HandNumber);
                if (!tempIsEvent)
                {
                    TurnState = Turn.ChoosingFieldPos;;
                    Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
                    //StartCoroutine(WaitingForField());
                }
                else
                {
                    //if event card, play from hand and not onto field.
                    SuspendedCard.GetComponent<Card>().SpecialAbility();
                    SuspendedCard = null;
                    Destroy(theChosenOne.gameObject);
                    TurnState = Turn.ChoosingFieldPos;
                }

            }
        }
    }

    IEnumerator WaitingForField()
    {
        while (TurnState == Turn.ChoosingFieldPos)
        {
            yield return 1;
        }
    }
    /// <summary>
    /// For When placing the card onto the field.
    /// </summary>
    /// <param name="spot">Chosen location on the field.</param>
    public void CardSpotChosen(int spot)
    {
        if (TurnState == Turn.ChoosingFieldPos)
        {
            if (IsPlayerTurn)
            {
                Go.Do("Spot Chosen: " + spot);
                PlayerField.PlaceCard(SuspendedCard, spot);
                //CHECK HERE IF WEIRD REPLACE SWAP BUG OCCURS
                TurnState = Turn.ActivatingAbilities;
                Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
            }
            else
            {
                Go.Do("AI Spot Chosen: " + spot);
                AIField.PlaceCard(SuspendedCard, spot);
                TurnState = Turn.ActivatingAbilities;
                Go.Do("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
            }
        }
    }

    public PlayField GetActiveField()
    {
        return IsPlayerTurn ? PlayerField : AIField;
    }

    public PlayField GetInactiveField()
    {
        return IsPlayerTurn ? AIField : PlayerField;
    }
}
