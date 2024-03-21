using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MultiButtons : MonoBehaviour
{
    // Start is called before the first frame update

    [Header("同时按下事件")]
    public UnityEvent syncPressEvent;

    List<bool> buttons;

    [Tooltip("按钮弹起延迟")]
    public float buttonReleaseDelay = 1f;
    [Tooltip("按钮数量")]
    public int buttonsNum;
    Queue<int> pressedQueue;

    private void Start()
    {
        buttons = new List<bool>();
        pressedQueue = new Queue<int>();
        for(int i=0;i<buttonsNum;i++)
        {
            buttons.Add(false);
        }
    }

    public void setButtonActive(int buttonId)
    {
        buttons[buttonId] = true;
        pressedQueue.Enqueue(buttonId);
        Invoke("setExpiredButtonInactive", buttonReleaseDelay);
    }

    public void setExpiredButtonInactive()
    {
        if(pressedQueue.Count != 0)
            buttons[pressedQueue.Dequeue()] = false;
    }

    private void Update()
    {
        bool result = true;
        foreach (bool buttonStat in buttons)
        {
            if(!buttonStat)
            {
                result = false;
                break;
            }
        }
        if(result == true)
        {
            syncPressEvent.Invoke();
        }

    }
}
