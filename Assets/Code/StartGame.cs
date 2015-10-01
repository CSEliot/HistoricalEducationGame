using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

    public Animator MoveButton;

    public void StartGameClick()
    {
        //Do Thing
        MoveButton.SetBool("Clicked", true);
    }
}
