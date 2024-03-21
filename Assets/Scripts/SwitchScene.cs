using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    CanvasGroup blackCanvas;
    public string nextLevelName;
    
    [Range(0f,5f)]
    [Tooltip("背景的淡出时间")]
    public float fadeTime = 1f;

    private void Awake()
    {
        blackCanvas = FindObjectOfType<DialogueManager>().blackCanvas;
        blackCanvas.alpha = 1f;
    }

    void Start()
    {
        LeanTween.alphaCanvas(blackCanvas, 0f, fadeTime);
    }

    public void NextLevel()
    {
        LeanTween.alphaCanvas(blackCanvas, 1f, fadeTime).setOnComplete(LoadLevel);
    }

    private void LoadLevel()
    {
        SceneManager.LoadSceneAsync(nextLevelName, LoadSceneMode.Single);
    }
}
