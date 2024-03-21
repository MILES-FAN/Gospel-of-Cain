using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElasticScaling : MonoBehaviour
{
    public float targetDistance;
    public float force;
    public float brakeFriction = 5f;
    public Rigidbody2D otherObjectRigid;
    Rigidbody2D thisRigid;
    public PhysicsMaterial2D brakeMat;
    

    private void Start()
    {
        thisRigid = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        float stretchedLength = Vector3.Distance(otherObjectRigid.gameObject.transform.position, this.transform.position) - targetDistance;
        if (stretchedLength > 0)
        {
            thisRigid.AddForce((otherObjectRigid.gameObject.transform.position - this.transform.position).normalized * force * stretchedLength * force * stretchedLength);
        }
    }
}
