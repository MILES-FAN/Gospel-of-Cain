using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ButtonIconStatus : MonoBehaviour
{
    public enum ButtonSpriteType
    {
        UI,Sprite
    }
    [SerializeField]
    public InputActionProperty actionProperty;
    InputAction action;
    [SerializeField]
    public ButtonSpriteType spriteType;
    Image image;
    SpriteRenderer spriteRenderer;
    public Sprite pressedIcon;
    public Sprite releasedIcon;
    Sprite currentSprite;

    private void Start()
    {
        action = actionProperty.action;
        if(spriteType == ButtonSpriteType.Sprite)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            currentSprite = spriteRenderer.sprite;
        }
        else
        {
            image = GetComponent<Image>();
            currentSprite = image.sprite;
        }
    }

    private void Update()
    {
        if (action.WasPressedThisFrame())
        {
            currentSprite = pressedIcon;
        }
        if (action.WasReleasedThisFrame())
        {
            currentSprite = releasedIcon;
        }
        if (spriteType == ButtonSpriteType.Sprite)
        {
            spriteRenderer.sprite = currentSprite;
        }
        else
        {
            image.sprite = currentSprite;
        }
    }
}
