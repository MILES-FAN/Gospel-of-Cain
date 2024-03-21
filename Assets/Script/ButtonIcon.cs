using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonIcon : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerInput playerInput;
    [SerializeField]
    GameObject controllerButton;
    [SerializeField]
    GameObject keyboardButton;
    void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
        changeSchemeIcon();
        //spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void changeSchemeIcon()
    {
        string scheme = playerInput.currentControlScheme;
        //Debug.Log(scheme);
        switch (scheme)
        {
            case "Gamepad":
                controllerButton.SetActive(true);
                keyboardButton.SetActive(false);
                break;
            case "Keyboard&Mouse":
                keyboardButton.SetActive(true);
                controllerButton.SetActive(false);
                break;
            default:
                keyboardButton.SetActive(true);
                controllerButton.SetActive(false);
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        changeSchemeIcon();
        
    }
}
