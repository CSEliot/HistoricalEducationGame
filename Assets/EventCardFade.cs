using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EventCardFade : MonoBehaviour {


    public Material myMat;
    private bool beginFading;
    private float myAlpha;
    
    // Use this for initialization
    void Start () {
        myAlpha = 1f;
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
        fadeCard.transform.GetChild(0).GetComponent<Image>().material = myMat;
        fadeCard.transform.GetChild(1).GetComponent<Image>().material = myMat;
        fadeCard.transform.GetChild(3).GetComponent<Image>().material = myMat;
        float FadeTime = 2f;
        fadeCard.transform.SetParent(transform, false);
        StartCoroutine(DestroyIn(FadeTime, fadeCard));
        beginFading = true;
    }

    private void FadeMaterial()
    {
        myAlpha = Mathf.Lerp(myAlpha, 0f, Time.deltaTime);
        myMat.SetColor("_Color", new Color(1f, 1f, 1f, myAlpha));
        Debug.Log("My alpha: " + myAlpha);
    }

    IEnumerator DestroyIn(float seconds, GameObject fadeCard)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(fadeCard);
        myAlpha = 1f;
        beginFading = false;
    }
}
