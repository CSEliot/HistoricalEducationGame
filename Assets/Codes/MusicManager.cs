using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour {

    public AudioClip[] Music;
    private AudioSource CDPlayer;

    private int currentTrack;
    private float startVolume;
    private float currentVolume;

	// Use this for initialization
	void Start () {
        CDPlayer = GetComponent<AudioSource>();
        CDPlayer.clip = Music[0];
        CDPlayer.Play();
        startVolume = CDPlayer.volume;
        currentVolume = startVolume;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private IEnumerator FadeMusicOut(int track)
    {
        while(currentVolume > 0.01f)
        {
            currentVolume = Mathf.Lerp(currentVolume, 0f, Time.deltaTime);
            CDPlayer.volume = currentVolume;
            yield return null;
            Debug.Log("Fading Volume Out: " + currentVolume);
        }
        Debug.Log("Fading Volume Out: " + currentVolume);
        CDPlayer.volume = 0f;
        CDPlayer.Stop();
        CDPlayer.clip = Music[track];
        CDPlayer.Play();
        currentTrack = track;
        StartCoroutine(FadeMusicIn());
    }

    private IEnumerator FadeMusicIn()
    {
        while(currentVolume < startVolume-0.01f)
        {
            currentVolume = Mathf.Lerp(currentVolume, startVolume, Time.deltaTime);
            CDPlayer.volume = currentVolume;
            yield return null;
            //Debug.Log("Fading Volume In: " + currentVolume);
        }
        //Debug.Log("Fading Volume In: " + currentVolume);
        CDPlayer.volume = startVolume;
        currentVolume = startVolume;
    }


    public void SetMusic(int track){
        //Debug.Log("Fading out track: " + currentTrack + " For new Track: "
            //+ track);
        if(track != currentTrack)
            StartCoroutine(FadeMusicOut(track));
    }

}
