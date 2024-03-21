using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class breathe : MonoBehaviour
{
    CanvasGroup canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<CanvasGroup>();
        BreatheIn();
    }

    void BreatheIn()
    {
        LeanTween.alphaCanvas(canvas, 0.7f, 2f).setDelay(1f).setOnComplete(BreatheOut);
    }

    void BreatheOut()
    {
        LeanTween.alphaCanvas(canvas, 1f, 2f).setDelay(1f).setOnComplete(BreatheIn);
    }
}
