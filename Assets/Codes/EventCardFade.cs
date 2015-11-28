using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EventCardFade : MonoBehaviour {


    public Material myMat;
    private bool beginFading;
    private float myAlpha;
    public float FadeTime;
    
    public void FadeObj(GameObject fadeCard)
    {

        if (!Application.loadedLevelName.Contains("Pop"))
        {
            fadeCard.transform.GetComponent<Image>().material = myMat;
            fadeCard.transform.GetChild(2).GetComponent<Image>().material = myMat;
        }
        else
        {
            fadeCard.transform.GetChild(0).GetComponent<Image>().material = myMat;
            fadeCard.transform.GetChild(1).GetComponent<Image>().material = myMat;
            fadeCard.transform.GetChild(3).GetComponent<Image>().material = myMat;
        }
        fadeCard.transform.SetParent(transform, false);
        fadeCard.transform.localPosition = Vector3.zero;
        myAlpha = 1f;
        StartCoroutine(fadeEnum(fadeCard));
    }

    private IEnumerator fadeEnum(GameObject fadeCard)
    {
        float startTime = Time.time;
        float endTime = startTime + FadeTime;
        while (Time.time < endTime)
        {

            myAlpha = Mathf.Lerp(1f, 0f, 
                (Time.time - startTime) / (endTime - startTime));
            myMat.SetColor("_Color", new Color(1f, 1f, 1f, myAlpha));
            yield return null;
        }
        Destroy(fadeCard);
        myAlpha = 1f;
    }
}
