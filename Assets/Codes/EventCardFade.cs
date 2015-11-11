using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EventCardFade : MonoBehaviour {


    public Material myMat;
    private bool beginFading;
    private float myAlpha;
    public float fadeTime;
    
    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void Update () {
        if (beginFading)
        {
            FadeMaterial();
        }
    }

    public void FadeObj(GameObject fadeCard)
    {
        if (!Application.loadedLevelName.Contains("Pop"))
        {
            fadeCard.transform.GetComponent<Image>().material = myMat;
            fadeCard.transform.GetChild(3).GetComponent<Image>().material = myMat;
        }
        else
        {
            fadeCard.transform.GetChild(0).GetComponent<Image>().material = myMat;
            fadeCard.transform.GetChild(1).GetComponent<Image>().material = myMat;
            fadeCard.transform.GetChild(3).GetComponent<Image>().material = myMat;
        }
        fadeCard.transform.SetParent(transform, false);
        fadeCard.transform.localPosition = Vector3.zero;
        myAlpha = fadeTime;
        beginFading = true;
        StartCoroutine(DestroyIn(fadeTime, fadeCard));
    }

    private void FadeMaterial()
    {
        myAlpha = Mathf.Lerp(myAlpha, 0f, Time.deltaTime);
        myMat.SetColor("_Color", new Color(1f, 1f, 1f, myAlpha/fadeTime));
        //Debug.Log("My alpha: " + myAlpha);
    }

    IEnumerator DestroyIn(float seconds, GameObject fadeCard)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(fadeCard);
        myAlpha = 1f;
        beginFading = false;
    }
}
