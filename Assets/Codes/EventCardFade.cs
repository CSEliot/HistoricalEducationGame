using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EventCardFade : MonoBehaviour
{


    public Material myMat;
    private bool beginFading;
    public float FadeTime;

    public void FadeObj(GameObject fadeCard)
    {
        float myAlpha;
        Material tempMat = new Material(myMat);
        
        if (!SceneManager.GetActiveScene().name.Contains("AB"))
        {
            fadeCard.transform.GetComponent<Image>().material = tempMat;
            fadeCard.transform.GetChild(2).GetComponent<Image>().material = tempMat;
        }
        else
        {
            fadeCard.transform.GetChild(0).GetComponent<Image>().material = tempMat;
            fadeCard.transform.GetChild(1).GetComponent<Image>().material = tempMat;
            fadeCard.transform.GetChild(3).GetComponent<Image>().material = tempMat;
        }
        fadeCard.transform.SetParent(transform, false);
        fadeCard.transform.localPosition = Vector3.zero;
        myAlpha = 1f;
        StartCoroutine(fadeEnum(fadeCard, tempMat, myAlpha));
    }

    private IEnumerator fadeEnum(GameObject fadeCard, Material tempMat, float myAlpha)
    {
        yield return new WaitForSeconds(2f);
        float startTime = Time.time;
        float endTime = startTime + FadeTime;
        while (Time.time < endTime)
        {
            myAlpha = Mathf.Lerp(1f, 0f,
                (Time.time - startTime) / (endTime - startTime));
            tempMat.SetColor("_Color", new Color(1f, 1f, 1f, myAlpha));
            yield return null;
        }
        Destroy(fadeCard);
        myAlpha = 1f;
    }

    public void NewGame()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}