using UnityEngine;
using System.Collections;

public class Go : MonoBehaviour {

	public bool DEBUG;
	public int MAX_LEVEL = 1;

	private static string ComponentName = "Debugger";
	
	public static void Do(object Out, int OutLevel)
	{
		
		if (GameObject.Find(ComponentName).GetComponent<Go>().DEBUG &&
			GameObject.Find(ComponentName).GetComponent<Go>().MAX_LEVEL <= OutLevel )
		{
			Debug.Log(Out);
		}
		
	}

	public static void Do(object Out)
	{
		if (GameObject.Find(ComponentName).GetComponent<Go>().DEBUG)
		{
			Debug.Log(Out);
		}
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
