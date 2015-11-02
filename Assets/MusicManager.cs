using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip[] Music;
    private AudioSource CDPlayer;

	// Use this for initialization
	void Start () {
        CDPlayer = GetComponent<AudioSource>();
        CDPlayer.clip = Music[0];
        CDPlayer.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetMusic(int track){
        CDPlayer.clip = Music[track];
    }

}
