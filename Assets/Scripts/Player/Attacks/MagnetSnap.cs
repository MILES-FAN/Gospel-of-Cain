using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MagnetSnap : MonoBehaviour
{
    PlayerGameInput playeInput;
    Rigidbody2D rigidbody;
    public bool snapping;

    private void Start()
    {
        playeInput = FindObjectOfType<PlayerGameInput>();
        rigidbody = GetComponent<Rigidbody2D>();
        playeInput.SnapAction.started += SnapPressed;
    }

    private void Update()
    {
        if(playeInput.SnapAction.WasReleasedThisFrame())
        {
            Release(new InputAction.CallbackContext());
        }
    }

    private void SnapPressed(InputAction.CallbackContext callback)
    {
        snapping = true;
    }

    public void FixToMagnet(Transform magnetTransform)
    {

        this.transform.LeanMove(magnetTransform.position, 0.1f);
        rigidbody.bodyType = RigidbodyType2D.Static;
    }



    private void Release(InputAction.CallbackContext callback)
    {
        Debug.Log("snap released");
        snapping = false;
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }
}
