using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class MusicManager : MonoBehaviour {

    public AudioClip[] Music;
    public AudioSource CDPlayer1;
    public AudioSource CDPlayer2;

    private bool fadingOut;
    private bool fadingIn;

    private int currentTrack;
    private int previousTrack;
    private float startVolume;
    private int currentCDPlayer;
    private int cdPlayer1Num = 1;
    private int cdPlayer2Num = 2;
    private float lerpExactnessBuffer = 0.02f;
    public float FadeTime;
 

    // Use this for initialization
    void Start () {

        currentCDPlayer = 1;
        CDPlayer1.clip = Music[0];
        CDPlayer1.Play();
        startVolume = CDPlayer1.volume;
        fadingOut = false;

        fadingIn = false;   
    }


    private IEnumerator FadeMusicIn(int track, AudioSource CDPlayer)
    {
        if (fadingIn)
            yield return null;

        fadingIn = true;
        CDPlayer.clip = Music[track];
        CDPlayer.Play();
        CDPlayer.volume = 0f;
        previousTrack = currentTrack;
        currentTrack = track;

        float targetVolume = startVolume;
        float startTime = Time.time;
        float finishTime = startTime + FadeTime + 0.01f;
        float currentVolume = CDPlayer.volume;
        while (Time.time < finishTime)
        {
            currentVolume = Mathf.Lerp(0f, targetVolume, 
                (Time.time - startTime) / (finishTime - startTime));
            CDPlayer.volume = currentVolume;
            yield return null;
            //Debug.Log("Fading Volume In: " + currentVolume);
        }
        //Debug.Log("Fading Volume In: " + currentVolume);
        CDPlayer.volume = 1.0f; // max volume
        fadingIn = false;
    }

    private IEnumerator FadeMusicOut(AudioSource CDPlayer)
    {
        if (fadingOut)
            yield return null;

        fadingOut = true;
        float targetVolume = 0f; ;
        float startTime = Time.time;
        float finishTime = startTime + FadeTime;
        float currentVolume = CDPlayer.volume;
        while (Time.time < finishTime)
        {
            currentVolume = Mathf.Lerp(startVolume, targetVolume,
                (Time.time - startTime) / (finishTime - startTime));
            CDPlayer.volume = currentVolume;
            yield return null;
            //Debug.Log("Fading Volume In: " + currentVolume);
        }
        //Debug.Log("Fading Volume In: " + currentVolume);
        CDPlayer.volume = 0.0f;
        CDPlayer.Stop();
        fadingOut = false;
    }


    public void SetMusic(int track){
        //Debug.Log("Fading out track: " + currentTrack + " For new Track: "
            //+ track);
        Debug.Log("Set Music called on Song: " + track);
        //Debug.Log("From: " + Environment.StackTrace);
        if (track != currentTrack)
        {
            if (currentCDPlayer == cdPlayer1Num)
            {
                StartCoroutine(FadeMusicOut(CDPlayer1));
                StartCoroutine(FadeMusicIn(track, CDPlayer2));
                currentCDPlayer = cdPlayer2Num;
            }
            else if (currentCDPlayer == cdPlayer2Num)
            {
                StartCoroutine(FadeMusicOut(CDPlayer2));
                StartCoroutine(FadeMusicIn(track, CDPlayer1));
                currentCDPlayer = cdPlayer1Num;
            }

        }
    }

    //go to previously played track
    public void Rewind()
    {
        SetMusic(previousTrack);
    }

}