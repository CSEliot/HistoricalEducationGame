using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EventCardFade : MonoBehaviour {


    public Material myMat;
    private bool beginFading;
    private float myAlpha;
    
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
        beginFading = true;
        StartCoroutine(DestroyIn(fadeCard));
    }

    private void FadeMaterial()
    {
        myAlpha = Mathf.Lerp(myAlpha, 0f, Time.deltaTime/2);
        myMat.SetColor("_Color", new Color(1f, 1f, 1f, myAlpha));
        //Debug.Log("My alpha: " + myAlpha);
    }

    IEnumerator DestroyIn(GameObject fadeCard)
    {
        while(myAlpha >= 0.3f)
            yield return null;
        Destroy(fadeCard);
        myAlpha = 1f;
        beginFading = false;
    }
}
