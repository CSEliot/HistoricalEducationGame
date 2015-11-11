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
        if (SFXNum >= SFXBank.Length)
            Debug.Log("SFX IS: " + SFXNum);
		SoundSource.PlayOneShot(SFXBank [SFXNum]);
	}
}