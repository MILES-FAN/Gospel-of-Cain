using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InteractableItems : MonoBehaviour
{
    [SerializeField]
    GameObject keySuggestion;
    public InputActionProperty actionProperty;
    [SerializeField]
    UnityEvent unityEvent;
    bool playerInside = false;

    private void Start()
    {
        actionProperty.action.performed += triggerEvent;
    }

    void triggerEvent(InputAction.CallbackContext callback)
    {
        if(playerInside)
        {
            Debug.Log("Yes!");
            unityEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(keySuggestion != null)
                keySuggestion.SetActive(true);
            playerInside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && keySuggestion != null)
        {
            if (keySuggestion != null)
                keySuggestion.SetActive(false);
            playerInside = false;
        }
    }
}
