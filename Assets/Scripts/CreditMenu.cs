using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CreditMenu : MonoBehaviour
{
    public CanvasGroup creditCanvas;
    public TMPro.TextMeshProUGUI creditText;
    public Transform tgt;
    [Range(10f, 300f)]
    public float duration = 40f;
    public UnityEvent unityEvent;

    public void StartCredit()
    {
        LeanTween.alphaCanvas(creditCanvas, 1f, 2f);
        LeanTween.move(creditText.gameObject, tgt.position, duration).setDelay(2f).setOnComplete(CompeleteAction);
    }

    private void CompeleteAction()
    {
        unityEvent.Invoke();
    }
}
