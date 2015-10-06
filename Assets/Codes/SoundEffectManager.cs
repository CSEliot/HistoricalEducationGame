using UnityEngine;
using System.Collections;

public class SoundEffectManager : MonoBehaviour {

	public AudioClip[] SFXBank;
	public AudioSource SoundSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(int SFXNum){
		SoundSource.PlayOneShot(SFXBank [SFXNum]);
	}
}