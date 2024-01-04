using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    bool fadein = false;
    bool fadeout = false;

    public float duration;

    // Update is called once per frame
    void Update()
    {
        if (fadein) {
            if(canvasGroup.alpha < 1) {
                canvasGroup.alpha += Time.deltaTime * duration;
                if(canvasGroup.alpha >= 1) {
                    fadein = false;
                }
            }
        }else if (fadeout) {
            if (canvasGroup.alpha > 0) {
                canvasGroup.alpha -= Time.deltaTime * duration;
                if (canvasGroup.alpha <= 0) {
                    fadeout = false;
                }
            }
        }

    }

    public void FadeIn() {
        fadein = true;
        fadeout = false;
    }

    public void FadeOut() {
        fadein = false;
        fadeout = true;
    }
}
