using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{
    public enum ColliderType
    {
        Player,Robot,Cart,Both
    }
    [Header("进入事件")]
    public UnityEvent enterEvent;
    [Header("滞留事件")]
    public UnityEvent stayEvent;
    [Header("离开事件")]
    public UnityEvent leaveEvent;
    [Header("触发类型")]
    [SerializeField] ColliderType colliderType = ColliderType.Both;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(DetectType(collision))
            enterEvent.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (DetectType(collision))
            stayEvent.Invoke();
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (DetectType(collision))
            leaveEvent.Invoke();
    }

    private bool DetectType(Collider2D collider)
    {
        switch(colliderType)
        {
            case ColliderType.Player:
                if (collider.gameObject.CompareTag("Player"))
                    return true;
                break;
            case ColliderType.Robot:
                if (collider.gameObject.CompareTag("Robot"))
                    return true;
                break;
            case ColliderType.Cart:
                if (collider.gameObject.CompareTag("cart"))
                    return true;
                break;
            case ColliderType.Both:
                if (collider.gameObject.CompareTag("Robot")|| collider.gameObject.CompareTag("Player") || collider.gameObject.CompareTag("cart"))
                    return true;
                break;
        }
        return false;
    }
}
