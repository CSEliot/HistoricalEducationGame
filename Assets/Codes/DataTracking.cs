using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Reflection;
using System.Net.NetworkInformation;

public class DataTracking : MonoBehaviour {

    private string startTime;
    private string filename;

    //AndroidJavaObject mWifiManager;
    
    // Use this for initialization
    void Start () {
        startTime = "";
        filename = Application.loadedLevelName + ".csv";

        //if the key in GETINT doesn't exist, it returns 0
        if (PlayerPrefs.GetInt("FirstLaunch") == 0)
        {
            ResetStats();
        }
        startTime += DateTime.Now.Second + ":";
        startTime += DateTime.Now.Minute + ":";
        startTime += DateTime.Now.Hour   + ":";
        startTime += DateTime.Now.Day    + ":";
        startTime += DateTime.Now.Month  + ":";
        startTime += DateTime.Now.Year;
        
    }
    
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown("p")){
            SaveData();
        }
    }

    public void WriteNewGameLine()
    {
        string path = GetPath(filename);
        FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(file);
        string LineToWrite = "GameName,MacAddress,NewGameTime(Sec:Min:Hr:Day:Mo:Yr),SessionEndTime,Progress,Rating,TotalPlayTime(Sec:Min:Hr:Days)";
        sw.WriteLine(LineToWrite);

        sw.Close();
        file.Close();

        ResetStats();
        //SAVED DATA

    }

    void OnApplicationQuit()
    {
        if(!Application.isEditor)
            PlayerPrefs.SetInt("IsFirstTime", 1);

        SaveData();
    }
    public void SaveData()
    {
        string path = GetPath(filename);
        FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
        StreamWriter sw = new StreamWriter(file);
        string LineToWrite;
        string GameName = Application.loadedLevelName;
        string MACAddress = GetMacAddress();
        string StartSession = startTime;
        string EndSession = GetEndSession();
        string GameProgess = PlayerPrefs.GetInt("Level") + "/15";
        string FunRating = GetFunRating();
        string TotalTime = GetTotalTimePlayed();
        LineToWrite = GameName + "," + MACAddress + "," +
            StartSession + "," + EndSession + "," + GameProgess + "," +
            FunRating + "," + TotalTime;
        sw.WriteLine(LineToWrite);

        sw.Close();
        file.Close();

        //SAVED DATA   
    }

    private void ResetStats()
    {
        //Debug.Log("Reset Data!");
        PlayerPrefs.SetInt("FirstDay", DateTime.Now.Day);
        PlayerPrefs.SetInt("FirstMonth", DateTime.Now.Month);
        PlayerPrefs.SetInt("FirstYear", DateTime.Now.Year);
        PlayerPrefs.SetInt("FirstSecond", DateTime.Now.Second);
        PlayerPrefs.SetInt("FirstMinute", DateTime.Now.Minute);
        PlayerPrefs.SetInt("FirstHour", DateTime.Now.Hour);
    }

    private string GetTotalTimePlayed()
    {
        DateTime oldTime = new DateTime(
            PlayerPrefs.GetInt("FirstYear"),
            PlayerPrefs.GetInt("FirstMonth"),
            PlayerPrefs.GetInt("FirstDay"),
            PlayerPrefs.GetInt("FirstHour"),
            PlayerPrefs.GetInt("FirstMinute"),
            PlayerPrefs.GetInt("FirstSecond"));
        TimeSpan span = DateTime.Now - oldTime;
        string timeString = "";
        timeString += span.Seconds + ":";
        timeString += span.Minutes + ":";
        timeString += span.Hours   + ":";
        timeString += span.Days;
        return timeString;
    }

    private string GetFunRating()
    {
        if (PlayerPrefs.GetInt("Rating") == 0)
        {
            return "not yet rated";
        }
        else
        {
            string rating = PlayerPrefs.GetInt("Rating") + "/5";
            return rating;
        }
    }

    private string GetEndSession()
    {
        string endSession = "";
        endSession += DateTime.Now.Second + ":";
        endSession += DateTime.Now.Minute + ":";
        endSession += DateTime.Now.Hour + ":";
        endSession += DateTime.Now.Day + ":";
        endSession += DateTime.Now.Month + ":";
        endSession += DateTime.Now.Year;
        return endSession;
    }

    private string GetPath(string filename)
    {
        string path;
        if (Application.platform == RuntimePlatform.Android)
        {
            try
            {
                Directory.CreateDirectory("/storage/emulated/0/Agora");
                Debug.Log("Making Directory: " + "/storage/emulated/0/Agora");
            }
            catch (Exception e)
            {
                Debug.Log("Try 1: " + e.StackTrace);
            }
            try
            {
                Directory.CreateDirectory("/storage/emulated/0/Agora/");
                Debug.Log("Making Directory: " + "/storage/emulated/0/Agora/");
            }
            catch (Exception e)
            {
                Debug.Log("Try 2: " + e.StackTrace);
            }
            Debug.Log("Attempted directory making . . .");
            return "/storage/emulated/0/Agora/" + filename;
        }
        else
        {
            path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, filename);
        }
    }
    

    private string GetMacAddress()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            // try to read the address from some file (this works on the Samsung Galaxy Tab 4 with Android 4.4.2)
            const string l_filePath = "/sys/class/net/wlan0/address"; // substitutions for wlan0: ip6gre0 ip6tnl0 lo p2p0 sit0 wlan0
            string l_contents = File.ReadAllText(l_filePath);
            Debug.Log("Read from \"" + l_filePath + "\": " + l_contents);
            l_contents = l_contents.Replace(Environment.NewLine, "");
            return l_contents;
        }
        else
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            string macAddress = "";

            foreach (NetworkInterface adapter in nics)
            {
                PhysicalAddress address = adapter.GetPhysicalAddress();
                byte[] bytes = address.GetAddressBytes();
                string mac = null;
                for (int i = 0; i < bytes.Length; i++)
                {
                    mac = string.Concat(mac + (string.Format("{0}", bytes[i].ToString("X2"))));
                    if (i != bytes.Length - 1)
                    {
                        mac = string.Concat(mac + "-");
                    }
                }
                macAddress += mac;
            }
            return macAddress;  
        }
    }
}
