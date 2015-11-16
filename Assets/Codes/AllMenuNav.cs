using UnityEngine;
using System.Collections;

/// <summary>
/// BUG: PRESSING "Search" in-cardgame and then returning then
/// quitting makes the menu navigating all weird . . . o.O
/// </summary>
public class AllMenuNav : MonoBehaviour {

    public int CurrentCanvasNum;
    public MusicManager MyMusicManager;

    public GameObject[] Canvases;

    // Use this for initialization
    void Start () {
        //disables all canvases in case I was messing around in one.
        //then enables the one I want to test in play mode via "CurrentCanvasNum"
        //game will be exported at Canvas 0.
        foreach (GameObject Canvas in Canvases)
        {
            Canvas.SetActive(false);
        }
        if (!Application.isEditor) { CurrentCanvasNum = 0; }
        Canvases[CurrentCanvasNum].SetActive(true);
    }
    
    // Update is called once per frame
    void Update () {
    }

    public void ChangeSceneTo(int MenuInt)
    {
        //Music is scene based, and so . . .
        MyMusicManager.SetMusic(0);
        
        if (MenuInt == -1)
        {
            Application.Quit();
        }
        Canvases[CurrentCanvasNum].SetActive(false);
        Canvases[MenuInt].SetActive(true);
        CurrentCanvasNum = MenuInt;
    }

    /// <summary>
    /// Only called by the level select buttons.
    /// </summary>
    /// <param name="MenuInt"></param>
    /// <param name="LevelInt"></param>
    public void ChangeSceneTo(int MenuInt, int LevelInt)
    {
        //Music is scene based, and so . . .
        int setMusicNum = LevelInt > 5 ? 2 : 1;
        setMusicNum = LevelInt > 10 ? 3 : setMusicNum;
        MyMusicManager.SetMusic(setMusicNum);

        if (MenuInt == -1)
        {
            Application.Quit();
        }
        Canvases[CurrentCanvasNum].SetActive(false);
        Canvases[MenuInt].SetActive(true);
        CurrentCanvasNum = MenuInt;
    }
}
