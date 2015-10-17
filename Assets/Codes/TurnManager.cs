using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour {

	public Hand PlayerHand;
	public Hand AIHand;

	private enum Turn{
		FillingHand,
		ChoosingCard,
		ActivatingAbilities,
		SwitchingTurn,
		Waiting
	};

	private Turn TurnState; 
	// Use this for initialization
	void Start () {
		TurnState = Turn.FillingHand;
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


}
