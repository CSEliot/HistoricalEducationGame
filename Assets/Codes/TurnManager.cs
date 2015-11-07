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
    public GameObject WinPanel;
    public GameObject LosePanel;
    public GameObject PlayerTurnText;
    public GameObject AITurnText;


    private bool IsPlayerTurn;
    private int CurrentLevel;
    private Transform SuspendedCard;

    //Every turn will check if it should activity an ability number here:
    private int[] activateAbilityAI = new int[] { -1, -1, -1, -1, -1, -1, -1};
    private int[] activateAbilityPlayer = new int[] { -1, -1, -1, -1, -1, -1, -1 }; 

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
        Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
    }
    
    // Update is called once per frame
    void Update () {
        if(TurnState == Turn.FillingHand){
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer(); 
            
            if (IsPlayerTurn)
            {
                //StartCoroutine("PlayerDrawCard");
                PlayerHand.DrawCard();
                TurnState = Turn.ChoosingCard;
                Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
            }
            else
            {
                //StartCoroutine("PlayerDrawCard");
                AIHand.DrawCard();
                TurnState = Turn.ChoosingCard;
                Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
            }
        }
        if (TurnState == Turn.Launching) {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer();

            AITurnText.SetActive(false);
            WinPanel.SetActive(false);
            LosePanel.SetActive(false);
            AIDeck.NewGame(CurrentLevel, LevelTracker.GetLevel());
            PlayerDeck.NewGame(CurrentLevel, LevelTracker.GetLevel());
            AIHand.NewGame();
            PlayerHand.NewGame();
            AIField.NewGame();
            PlayerField.NewGame();
            InfluenceManager.NewGame();
            //play teacher lady info on gameplay
            //wait for her to finish

            TurnState = Turn.FillingHand;
            Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
        }
        if (TurnState == Turn.ActivatingAbilities)
        {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer(); 
            
            if (IsPlayerTurn)
            {
                StartCoroutine(PlayerField.ActivateCardsOnField());
            }
            else
            {
                StartCoroutine(AIField.ActivateCardsOnField());
            }
            TurnState = Turn.Waiting;
            Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
        }
        if (TurnState == Turn.SwitchingTurn)
        {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer();
            
            //i really should not be doing this every turn . . .
            InfluenceManager temp = GameObject.FindGameObjectWithTag("InfluenceManager").
            GetComponent<InfluenceManager>();
            int winCon = temp.GetWinStatus();
            if (winCon == 0)
            {
                //influence manager has to flip adding or subtracting points.
                temp.TurnChange();
                IsPlayerTurn = !IsPlayerTurn;
                Debug.Log("Turn Change to: " + (IsPlayerTurn ? "Player":"AI"));
                TurnState = Turn.FillingHand;
                Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
                //no winner yet
                if (IsPlayerTurn)
                {
                    PlayerTurnText.SetActive(true);
                    AITurnText.SetActive(false);
                }
                else
                {
                    PlayerTurnText.SetActive(false);
                    AITurnText.SetActive(true);
                }
            }
            else if (winCon == 1) 
            {
                //PLAYER WON
                GameObject.FindGameObjectWithTag("MenuController").
                    GetComponent<LevelTracking>().LevelUp();
                WinPanel.SetActive(true);
                TurnState = Turn.Waiting;
            }
            else if (winCon == -1)
            {
                LosePanel.SetActive(true);
            }
        }
        if (TurnState == Turn.ChoosingCard)
        {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer();
            
            if (!IsPlayerTurn)
            {
                //AI can't press buttons, so we press for it.
                HandPlayed(AIHand.GetRandomCard(), 0);
            }
        }
        if (TurnState == Turn.ChoosingFieldPos)
        {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer(); 
            
            if (!IsPlayerTurn)
            {
                //AI can't press buttons, so we press for it.
                CardSpotChosen(UnityEngine.Random.Range(0, 5));
            }
        }
    }

    IEnumerator CardPlayed(){
        yield return new WaitForSeconds(2f);
        //wait for abilities to go off.
        TurnState = Turn.SwitchingTurn; Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
    }

    public void EndTurn(){
        TurnState = Turn.SwitchingTurn;
        Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
        GameObject.FindGameObjectWithTag("SFXController").
                GetComponent<SoundEffectManager>().PlaySound(15);
    }

    public void Launch(int StageNum)
    {
        IsPlayerTurn = true;
        CurrentLevel = StageNum;
        TurnState = Turn.Launching;
        Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
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
                Debug.Log("HandNumber played by Player: " + HandNumber);
                //handnumber tells us which one to remove from 
                // hand (not delete)
                PlayerHand.RemoveCard(HandNumber);
                if (!tempIsEvent)
                {
                    TurnState = Turn.ChoosingFieldPos;
                    Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
                    
                    //StartCoroutine(WaitingForField());
                }
                else
                {
                    Debug.Log("Event Played");
                    //if event card, play from hand and not onto field.
                    SuspendedCard.gameObject.SetActive(true);
                    //SuspendedCard.transform.localScale = new Vector3(5f, 5f, 5f);
                    SuspendedCard.GetComponent<Card>().SpecialAbility();
                    SuspendedCard = null;
                    GameObject.FindGameObjectWithTag("EventHolderYours").
                        GetComponent<EventCardFade>().FadeObj(theChosenOne.gameObject);
                    TurnState = Turn.ActivatingAbilities;
                }
            }
            else
            {
                AIHand.RemoveCard(HandNumber);
                if (!tempIsEvent)
                {
                    TurnState = Turn.ChoosingFieldPos;;
                    Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
                    //StartCoroutine(WaitingForField());
                }
                else
                {
                    //if event card, play from hand and not onto field.
                    SuspendedCard.GetComponent<Card>().SpecialAbility();
                    SuspendedCard = null;
                    GameObject.FindGameObjectWithTag("EventHolderAI").
                        GetComponent<EventCardFade>().FadeObj(theChosenOne.gameObject);
                    TurnState = Turn.ActivatingAbilities;
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
                //Debug.Log("Spot Chosen: " + spot);
                PlayerField.PlaceCard(SuspendedCard, spot);
                //CHECK HERE IF WEIRD REPLACE SWAP BUG OCCURS
                TurnState = Turn.ActivatingAbilities;
                Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
            }
            else
            {
                Debug.Log("AI Spot Chosen: " + spot);
                AIField.PlaceCard(SuspendedCard, spot);
                TurnState = Turn.ActivatingAbilities;
                Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
            }
        }
        //SuspendedCard.localPosition = Vector3.zero; // a brute force fix on 
        //                                            // weird location bug.
    }

    public PlayField GetField(bool GetAIField)
    {
        if (GetAIField)
        {
            return AIField;
        }
        else
        {
            return PlayerField;
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

    public bool GetIsPlayerTurn()
    {
        return IsPlayerTurn;
    }

    public void SetNextAbility(bool forAI, int turnNum, int abilityNum)
    {
        if (forAI)
        {
            activateAbilityAI[turnNum] = abilityNum;
        }
        else
        {
            activateAbilityPlayer[turnNum] = abilityNum;
        }
    }

    private void CheckAbilityEnableAI()
    {
        int abilityNum = activateAbilityAI[(int)TurnState];
        if (abilityNum != -1)
        {
            AllSpecialFunctions.TestAbility(abilityNum, 0);
            activateAbilityAI[(int)Turn.SwitchingTurn] = -1;
        }
    }

    private void CheckAbilityEnablePlayer()
    {
        int abilityNum = activateAbilityPlayer[(int)TurnState];
        if (abilityNum != -1)
        {
            AllSpecialFunctions.TestAbility(abilityNum, 0);
            activateAbilityPlayer[(int)Turn.SwitchingTurn] = -1;
        }
    }
}
