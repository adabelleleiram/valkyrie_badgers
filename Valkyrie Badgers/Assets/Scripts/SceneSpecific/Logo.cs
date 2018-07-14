using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Logo : MonoBehaviour {

    public float showTime;
    public float fadeTime;

    float currentTime = 0;

	// Use this for initialization
	void Start () {
        if (GameHandler.persistencyManager.shownLogo)
            gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;

        float alpha = 1 - Mathf.Clamp01((currentTime - showTime)/ showTime);
        GetComponent<Image>().color = new Vector4(1, 1, 1, alpha);

        if (alpha == 0)
        {
            GameHandler.persistencyManager.shownLogo = true;
            gameObject.SetActive(false);
        }

    }
}
