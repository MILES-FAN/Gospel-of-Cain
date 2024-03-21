using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStoper : MonoBehaviour
{
    public StickToPoint sticker;
    public LayerMask groundLayer;
    Vector3 oldPos, newPos;
    // Start is called before the first frame update

    private void Start()
    {
        newPos = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        sticker.Disconnect();
    }

    private void FixedUpdate()
    {
        oldPos = newPos;
        newPos = transform.position;
        Vector3 motionDir = newPos - oldPos;
        //Debug.Log(motionDir);
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, 0.3f, motionDir, 1f + Vector3.Distance(newPos,oldPos), groundLayer);
        if (hit2D.collider != null)
        {
            sticker.Disconnect();
        }
    }
}
