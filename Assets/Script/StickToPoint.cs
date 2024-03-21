using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPoint : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D otherObjectRigid;
    [HideInInspector] bool connected = false;

    public void Connect()
    {
        connected = true;
        this.transform.position = otherObjectRigid.position;
        otherObjectRigid.bodyType = RigidbodyType2D.Kinematic;
    }

    public void Disconnect()
    {
        connected = false;
        otherObjectRigid.bodyType = RigidbodyType2D.Dynamic;
    }

    private void Update()
    {
        if(connected)
        {
            otherObjectRigid.position = this.transform.position;
        }
    }


}
