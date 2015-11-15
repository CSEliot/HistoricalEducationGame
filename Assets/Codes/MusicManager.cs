using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class MusicManager : MonoBehaviour {

    public AudioClip[] Music;
    private AudioSource CDPlayer;

    private bool FadingOut;

    private int currentTrack;
    private int previousTrack;
    private float startVolume;
    private float currentVolume;
    // Use this for initialization
    void Start () {
        
        CDPlayer = GetComponent<AudioSource>();
        CDPlayer.clip = Music[0];
        CDPlayer.Play();
        startVolume = CDPlayer.volume;
        currentVolume = startVolume;
        FadingOut = false;
    }
    
    // Update is called once per frame
    void Update () {
    
    }



    private IEnumerator FadeMusicIn(int track)
    {
        CDPlayer.volume = 0f;
        CDPlayer.Stop();
        CDPlayer.clip = Music[track];
        CDPlayer.Play();
        previousTrack = currentTrack;
        currentTrack = track;

        CDPlayer.volume = 0f;
        while(currentVolume < startVolume-0.1f)
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
        Debug.Log("Set Music called on Song: " + track);
        //Debug.Log("From: " + Environment.StackTrace);
        if (track != currentTrack)
            StartCoroutine(FadeMusicIn(track));
    }

    public void Rewind()
    {
        SetMusic(previousTrack);
    }

}
//private IEnumerator FadeMusicOut(int track, float startTime)
//{
//    FadingOut = true;
//    while(currentVolume > 0.1f)
//    {
//        currentVolume = Mathf.Lerp(currentVolume, 0f, Time.deltaTime);
//        CDPlayer.volume = currentVolume;
//        yield return null;
//        Debug.Log("Fading Volume Out: " + currentVolume);
//    }
//    Debug.Log("Fading Volume Out: " + currentVolume);
//    CDPlayer.volume = 0f;
//    CDPlayer.Stop();
//    CDPlayer.clip = Music[track];
//    CDPlayer.Play();
//    previousTrack = currentTrack;
//    currentTrack = track;
//    FadingOut = false;
//    StartCoroutine(FadeMusicIn());
//}