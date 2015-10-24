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

	// Use this for initialization
	void Start () {
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

    public void SaveData()
    {
        string path = GetPath(filename);
        FileStream file = new FileStream (path, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter( file );
        //Player level is based on length of string! xD (didn't feel like converting)
        string levelStringTemp = "";
        //since the letter doesn't matter, we get a random one.
        for (int i = 0; i < PlayerLevel; i++)
        {
            levelStringTemp += GetRandomLetter();
        }
        sw.WriteLine(levelStringTemp);

        sw.Close();
        file.Close();
        //SAVED DATA
    }

    public void LoadData()
    {
        string path = GetPath( filename );

        if (File.Exists(path))
        {
            FileStream file = new FileStream (path, FileMode.Open, FileAccess.Read);
            StreamReader sr = new StreamReader( file );

            string str = null;
            str = sr.ReadLine ();

            sr.Close();
            file.Close();

            PlayerLevel = str.Length;
        }else
        {
            PlayerLevel = 0;
            SaveData();
        }
    }

    private string GetPath(string filename)
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            path = Application.persistentDataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
        else
        {
            path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }

    private static char GetRandomLetter()
    {
        System.Random _random = new System.Random();
        int randInt = _random.Next(0, 26); // Zero to 25
        char randLetter = (char)('a' + randInt);
        return randLetter;
    }
}
