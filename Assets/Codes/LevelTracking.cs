using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Reflection;
public class LevelTracking : MonoBehaviour {

    private int PlayerLevel;
    private bool DataLoaded;
    private string filename = "SaveData.data";
    public int TempLevel;

    // Use this for initialization
    void Start () {
        if (Application.isEditor)
        {
            PlayerLevel = TempLevel;
            SaveData();
        }
        DataLoaded = false;
        LoadData();
        DataLoaded = true;

    }
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("s"))
        {
            SaveData();
        }

    }

    public void LevelUp()
    {
        PlayerLevel++;
        SaveData();
    }

    public int GetLevel()
    {
        return PlayerLevel;
    }

    public void ResetStats()
    {
        PlayerLevel = 0;
        PlayerPrefs.SetInt("level", PlayerLevel);
        PlayerPrefs.SetInt("Rating", 0);
        GetComponent<DataTracking>().WriteNewGameLine();
        
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("level", PlayerLevel);
    }

    public void LoadData()
    {
        PlayerLevel = PlayerPrefs.GetInt("level");
    }

    private static char GetRandomLetter()
    {
        System.Random _random = new System.Random();
        int randInt = _random.Next(0, 26); // Zero to 25
        char randLetter = (char)('a' + randInt);
        return randLetter;
    }
}
