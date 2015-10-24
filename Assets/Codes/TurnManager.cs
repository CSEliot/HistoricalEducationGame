using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	public Hand PlayerHand;
	public Hand AIHand;
    public Deck PlayerDeck;
    public Deck AIDeck;
    public PlayField PlayerField;
    public PlayField AIField;
    public LevelTracking LevelTracker;
    public InfluenceManager InfluenceManager;

	private enum Turn{
        Launching,
		FillingHand,
		ChoosingCard,
		ActivatingAbilities,
		SwitchingTurn,
		Waiting
	};

	private Turn TurnState; 
	// Use this for initialization
	void Start () {
		TurnState = Turn.Launching;
	}
	
	// Update is called once per frame
	void Update () {
		if(TurnState == Turn.FillingHand){
			//StartCoroutine("PlayerDrawCard");
			TurnState = Turn.Waiting;
		}
	}

	IEnumerator PlayerDrawCard(){

		TurnState = Turn.ChoosingCard;
		yield return null;
	}

	private void WaitForCardChoice(){
	}

	private void EndTurn(){
	}

	private void ActivateCardAbilties(){
	}

    public void Launch(int StageNum)
    {
        TurnState = Turn.Launching;
        AIDeck.NewGame(StageNum);
        PlayerDeck.NewGame(StageNum);
        AIHand.NewGame();
        PlayerHand.NewGame();
        AIField.NewGame();
        PlayerField.NewGame();
        InfluenceManager.NewGame();
    }

}
