using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpPixelAlpha : MonoBehaviour {

    public Image FadeMat;

    public float AlphaSpeed;
    private float AlphaValue;
    private Color ImageFade;
	// Use this for initialization
	void Start () {
        AlphaValue = 1;
        ImageFade = new Color(255f, 255f, 255f, 255f);
	}
	
	// Update is called once per frame
	void Update () {
        AlphaValue = AlphaValue > 0f ? AlphaValue-AlphaSpeed : 0f;
        ImageFade.a = AlphaValue;
        FadeMat.color = ImageFade;
        if (AlphaValue <= 0.1f)
        {
            transform.gameObject.SetActive(false);
        }
	}
}
