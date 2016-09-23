using UnityEngine;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Reflection;
using System.Net.NetworkInformation;
using UnityEngine.SceneManagement;

public class DataTracking : MonoBehaviour {

    private string startTimeString;
    private DateTime startTimeOBJ;
    private string filename;

    //AndroidJavaObject mWifiManager;
    
    // Use this for initialization
    void Start () {
        startTimeString = "";
        filename = SceneManager.GetActiveScene().name + ".csv";

        startTimeString += DateTime.Now.Second + ":";
        startTimeString += DateTime.Now.Minute + ":";
        startTimeString += DateTime.Now.Hour   + ":";
        startTimeString += DateTime.Now.Day    + ":";
        startTimeString += DateTime.Now.Month  + ":";
        startTimeString += DateTime.Now.Year;
        startTimeOBJ = DateTime.Now;
    }
    
    // Update is called once per frame
    void Update () {
        if(Input.GetKeyDown("p")){
            SaveData(false);
        }
    }

    public void WriteNewGameLine()
    {
        return;
        //string path = GetPath(filename);
        //FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
        //StreamWriter sw = new StreamWriter(file);
        //string LineToWrite = "New_Game_Started!,\nGameName,MacAddress,NewGameTime(Sec:Min:Hr:Day:Mo:Yr),SessionEndTime,Progress,Rating,TotalPlayTime(Sec:Min:Hr:Days)";
        //sw.WriteLine(LineToWrite);

        //sw.Close();
        //file.Close();
        //SAVED DATA
    }

    void OnApplicationQuit()
    {
        if (!Application.isEditor)
            PlayerPrefs.SetInt("IsFirstTime", 1);
        PlayerPrefs.Save();
    }

    public void SaveData(bool onClose)
    {
        PlayerPrefs.Save();

        return;

        //string path = GetPath(filename);
        //FileStream file = new FileStream(path, FileMode.Append, FileAccess.Write);
        //StreamWriter sw = new StreamWriter(file);
        //string LineToWrite = "";
        //if (onClose)
        //{
        //    LineToWrite = "Following_line_saved_by_force_quit.Please_quit_via_In-game_Button.\n";
        //}
        //string GameName = SceneManager.GetActiveScene().name;
        //string MACAddress = GetMacAddress();
        //string StartSession = startTimeString;
        //string EndSession = GetEndSession();
        //string GameProgess = PlayerPrefs.GetInt("Level") + " out of 15";
        //string FunRating = GetFunRating();
        //string TotalTime = GetTotalTimePlayed();
        //LineToWrite += GameName + "," + MACAddress + "," +
        //    StartSession + "," + EndSession + "," + GameProgess + "," +
        //    FunRating + "," + TotalTime;
        //sw.WriteLine(LineToWrite);

        //sw.Close();
        //file.Close();
        //PlayerPrefs.Save();

        //SAVED DATA   
    }


    private string GetTotalTimePlayed()
    {
        
        TimeSpan span = new TimeSpan(
            PlayerPrefs.GetInt("TotalDay"),
            PlayerPrefs.GetInt("TotalHour"),
            PlayerPrefs.GetInt("TotalMinute"),
            PlayerPrefs.GetInt("TotalSecond"));
        //oldTime = new DateTime()
        span += (DateTime.Now - startTimeOBJ);
        startTimeOBJ = DateTime.Now; // reset because otherwise we're adding on
                                     //old time already recorded in this session of play.

        //The following occurs because we know that 
        //PlayerPrefs.Save() happens later on.
        PlayerPrefs.SetInt("TotalDay", span.Days);
        PlayerPrefs.SetInt("TotalHour", span.Hours);
        PlayerPrefs.SetInt("TotalMinute", span.Minutes);
        PlayerPrefs.SetInt("TotalSecond", span.Seconds);
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
            return "Not yet rated.";
        }
        else
        {
            string rating = PlayerPrefs.GetInt("Rating") + " stars out of 5.";
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

    public void ResetSessionStartDateTime()
    {
        startTimeOBJ = DateTime.Now;
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
