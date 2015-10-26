using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ClearedText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnEnable()
    {
        Color Full = new Color(1f, 1f, 1f, 1f);
        Color Dark = new Color(0f, 0f, 0f, 0f);
        float FadeTime = 2f;
        GetComponent<Text>().CrossFadeColor(Full, 0f, false, true);
        GetComponent<Text>().CrossFadeColor(Dark, FadeTime, false, true);
        StartCoroutine(DisableIn(FadeTime));
    }

    IEnumerator DisableIn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.SetActive(false);
    }
}
