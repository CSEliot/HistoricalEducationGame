using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
public class TurnManager : MonoBehaviour {

    public Hand PlayerHand;
    public Hand AIHand;
    public Deck PlayerDeck;
    public Deck AIDeck;
    public PlayField PlayerField;
    public PlayField AIField;
    public LevelTracking LevelTracker;
    public InfluenceManager MyInfluenceManager;
    public GameObject WinPanel1;
    public GameObject WinPanel2;
    public GameObject WinPanel3;
    public GameObject LosePanel;
    public GameObject PlayerTurnText;
    public GameObject AITurnText;
    public MusicManager MyMusicManager;

    public GameObject LevelObject;

    private bool IsPlayerTurn;
    private int CurrentLevel;
    private Transform SuspendedCard;
    private bool giveTurnlyInfluenceAI;
    private bool giveTurnlyInfluencePlayer;

    //Every turn will check if it should activity an ability number here:
    private int[] activateAbilityAI = new int[] { -1, -1, -1, -1, -1, -1, -1};
    private int[] activateAbilityPlayer = new int[] { -1, -1, -1, -1, -1, -1, -1 }; 

    //Instead of implementing my own AI class, i'm going to implement certain 
    //AI behaviors right where the relevant code is . . .
    private int[] IsDoubleList = new int[] { 0, 0, 0, 0, 0, 1, 1, 0, 1, 0, 0,
                                             0, 0, 0, 1, 0, 0, 1, 0, 1, 0};

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
                if (giveTurnlyInfluencePlayer)
                {
                    MyInfluenceManager.IncreaseInfluence(1);
                }
            }
            else
            {
                //StartCoroutine("PlayerDrawCard");
                AIHand.DrawCard();
                TurnState = Turn.ChoosingCard;
                Debug.Log("Turnstate is now: " + Enum.GetName(typeof(Turn), TurnState));
                if (giveTurnlyInfluenceAI)
                {
                    MyInfluenceManager.IncreaseInfluence(1);
                }
            }
        }
        if (TurnState == Turn.Launching) {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer();
            giveTurnlyInfluenceAI = false;
            giveTurnlyInfluencePlayer = false;


            AITurnText.SetActive(false);
            WinPanel1.SetActive(false);
            WinPanel2.SetActive(false);
            WinPanel3.SetActive(false);
            LosePanel.SetActive(false);
            AIDeck.NewGame(CurrentLevel, LevelTracker.GetLevel());
            PlayerDeck.NewGame(CurrentLevel, LevelTracker.GetLevel());
            AIHand.NewGame();
            PlayerHand.NewGame();
            AIField.NewGame();
            PlayerField.NewGame();
            MyInfluenceManager.NewGame();
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
            MyInfluenceManager.CancelDouble();

            int winCon = MyInfluenceManager.GetWinStatus();
            if (winCon == 0)
            {
                //influence manager has to flip adding or subtracting points.
                MyInfluenceManager.TurnChange();
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
                //if it's a new stage, unlock new card notice
                if (LevelTracker.GetLevel() <= 13 &&
                    CurrentLevel == LevelTracker.GetLevel() + 1)
                {
                    GameObject.FindGameObjectWithTag("MenuController").
                        GetComponent<LevelTracking>().LevelUp();
                    WinPanel1.SetActive(true);
                    TurnState = Turn.Waiting;
                }
                else if (LevelTracker.GetLevel() != 14)
                {
                    WinPanel2.SetActive(true);
                    TurnState = Turn.Waiting;
                }
                else if (LevelTracker.GetLevel() == 14)
                {
                    //activate panel on congratulate the player on beating the 
                    //game.
                    GameObject.FindGameObjectWithTag("MenuController").
                        GetComponent<LevelTracking>().LevelUp();
                    WinPanel3.SetActive(true);
                    TurnState = Turn.Waiting;
                }
            }
            else if (winCon == -1)
            {
                LosePanel.SetActive(true);
            }

            GameObject.FindGameObjectWithTag("InfluenceManager").
                GetComponent<InfluenceManager>().DisableSpoilsInfluence();
        }
        if (TurnState == Turn.ChoosingCard)
        {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer();
            
            if (!IsPlayerTurn)
            {
                //AI can't press buttons, so we press for it.
                Transform fromAIHand = AIHand.GetRandomCard();
                HandPlayed(fromAIHand, fromAIHand.GetComponent<Card>().GetNumPos());
            }
        }
        if (TurnState == Turn.ChoosingFieldPos)
        {
            CheckAbilityEnableAI();
            CheckAbilityEnablePlayer(); 
            
            if (!IsPlayerTurn)
            {
                //AI can't press buttons, so we press for it.
                int cardType = SuspendedCard.GetComponent<Card>().GetCardType();
                int playLocation = -1;
                bool hasDoubleAbility = (IsDoubleList[cardType] == 1);
                Debug.Log("AI Card: "  + cardType + "Is Double: " + hasDoubleAbility);
                if (hasDoubleAbility)
                {
                    //doubling cards shouldn't be placed on the right
                    do
                    {
                        Debug.Log("Got Play Location: " + playLocation);
                        playLocation = AIField.GetEmptiestLocation();
                    } while (playLocation == 4);
                }
                else
                {
                    //influence giving cards shouldn't be placed on the left
                    do
                    {
                        Debug.Log("Got Play Location: " + playLocation);
                        playLocation = AIField.GetEmptiestLocation();
                    } while (playLocation == 0);
                }
                CardSpotChosen(playLocation);
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
        if (Application.loadedLevelName.Contains("Pop"))
        {
            GameObject.FindGameObjectWithTag("SFXController").
                    GetComponent<SoundEffectManager>().PlaySound(15);
        }
        else
        {
            if (IsPlayerTurn)
            {
                GameObject.FindGameObjectWithTag("SFXController").
                        GetComponent<SoundEffectManager>().PlaySound(15);
            }
            else
            {
                GameObject.FindGameObjectWithTag("SFXController").
                        GetComponent<SoundEffectManager>().PlaySound(18);
            }
        }
    }

    public void Launch(int StageNum)
    {
        if (Application.loadedLevelName.Contains("Pop"))
        {
            LevelObject.GetComponent<Text>().text = GetText(StageNum);
            LevelObject.SetActive(true);
        }

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
                //Debug.Log("HandNumber played by Player: " + HandNumber);
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
                    if (SuspendedCard.GetComponent<Card>().GetCardType() != 17)
                    {
                        TurnState = Turn.ActivatingAbilities;
                    }
                    GameObject.FindGameObjectWithTag("EventHolderYours").
                        GetComponent<EventCardFade>().FadeObj(theChosenOne.gameObject);
                    SuspendedCard = null;
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
                    //test card type. if it's #17, don't activate it, instead,
                    if (SuspendedCard.GetComponent<Card>().GetCardType() == 17)
                    {
                        //recall HandPlayed() with either a new double or
                        // +2 influence card. 
                        int chosenCard = UnityEngine.Random.Range(1, 3) == 1?1:5;
                        HandPlayed( AIDeck.BuildClone(chosenCard).transform, -1);
                    }
                    else
                    {
                        Debug.Log("AI Event Played");
                        //if event card, play from hand and not onto field.
                        SuspendedCard.gameObject.SetActive(true);
                        SuspendedCard.GetComponent<Card>().SpecialAbility();
                        GameObject.FindGameObjectWithTag("EventHolderAI").
                            GetComponent<EventCardFade>().FadeObj(theChosenOne.gameObject);
                        SuspendedCard = null;
                        TurnState = Turn.ActivatingAbilities;
                    }
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

    public void EnablePermaInfluenceBoost(bool forAI)
    {
        if (forAI)
        {
            giveTurnlyInfluenceAI = true;
        }
        else
        {
            giveTurnlyInfluencePlayer = true;
        }
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

    /// <summary>
    /// For turn and phase based ability activation
    /// </summary>
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

    /// <summary>
    /// SHOULD ONLY BE CALLED During Turn.ChoosingFieldPos
    /// </summary>
    /// <param name="newCard"></param>
    public void SetSuspendedCard(Transform newCard)
    {
        SuspendedCard = newCard;
    }

    private string GetText(int levelNum)
    {
        switch (levelNum)
        {
            case 1:
                return "Level 1:\nLouisiana";
            case 2:
                return "Level 2:\nMississippi";
            case 3:
                return "Level 3:\nAlabama";
            case 4:
                return "Level 4:\nGeorgia";
            case 5:
                return "Level 5:\nS. Carolina";
            case 6:
                return "Level 6:\nTennessee";
            case 7:
                return "Level 7:\nN. Carolina";
            case 8:
                return "Level 8:\nKentucky";
            case 9:
                return "Level 9:\nVirginia";
            case 10:
                return "Level 10:\nMaryland";
            case 11:
                return "Level 11:\nIllinois";
            case 12:
                return "Level 12:\nIndiana";
            case 13:
                return "Level 13:\nOhio";
            case 14:
                return "Level 14:\nPennsylvania";
            case 15:
                return "Level 15:\nNew York";

            default:
                Debug.LogError("BAD LEVELNUM GIVEN");
                return null;
        }
    }
}
