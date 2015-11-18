using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonLevelManager : MonoBehaviour {

    public LevelTracking LevelClass;
    int unlockedLevels;

    //public Sprite played;
    //public Sprite latest;

    // Use this for initialization
    void Start () {
        //disable ALL buttons, then enable unlocked
        DisableAllButtons();
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        if (unlockedLevels != LevelClass.GetLevel()+1)
        {
            DisableAllButtons();
            UpdateButtonUnlocks();
            Debug.Log("Level Change Detected");
        }
    }

    private void DisableAllButtons()
    {
        for (int x = 4; x >= 0; x--)
        {
            //x = 5 columns of buttons
            for (int y = 2; y >= 0; y--)
            {
                //y = 3 buttons per column
                //Debug.Log("Disabling: x: " + x + " y:" + y);
                transform.GetChild(x).GetChild(y).GetComponent<Button>().
                    interactable = false;
                //transform.GetChild(x).GetChild(y).GetComponent<Button>().
                //    image.sprite = played;
            }
        }
    }

    private void UpdateButtonUnlocks()
    {
        int unlockedLevelsCalc; //used purely for mathing buttons out.
        unlockedLevels = LevelClass.GetLevel() + 1;//levels represented 1-15
        unlockedLevelsCalc = (unlockedLevels >= 15) ? 15 : unlockedLevels;
        int activeColumns = Mathf.CeilToInt(((float)unlockedLevelsCalc) / 3f);
        activeColumns = (unlockedLevelsCalc == 0) ? 1 : activeColumns;
        int buttonsInColumn = unlockedLevelsCalc - ((activeColumns - 1) * 3);
        Debug.Log("At level stage: " + unlockedLevels +
            ", Columns Unlocked: " + activeColumns + ", Buttons: "
            + buttonsInColumn); 
        for (int x = activeColumns-2; x >= 0; x--)
        {
            //x = 5 columns of buttons
            for (int y = 2; y >= 0; y--)
            {
                //y = 3 buttons per column
                //Debug.Log("Enabling: x: " + x + " y:" + y);
                transform.GetChild(x).GetChild(y).GetComponent<Button>().
                    interactable = true;
            }
        }
        for (int x = 0; x < buttonsInColumn; x++)
        {
            transform.GetChild(activeColumns-1).GetChild(x).GetComponent<Button>().
                    interactable = true;
            //if(Application.loadedLevelName.Contains("Pop"))
            //if (x == buttonsInColumn - 1)
            //{
            //    transform.GetChild(activeColumns - 1).GetChild(x).GetComponent<Button>().
            //        image.sprite = latest;
            //}
        }
    }


}
