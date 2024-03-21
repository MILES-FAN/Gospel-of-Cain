using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackControllable : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    [HideInInspector]
    public TrackRide ride;
    //[SerializeField,Range(0,5f)]
    //float snapForce = 2.5f;
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
    public void Snap(Vector3 position,float snapForce)
    {
        if(ride != null)
        {
            ride.velocity = ride.velocity + ((Vector2)(position - transform.position)).normalized * snapForce * 60 * Time.deltaTime;
            ride.gravityScale = 0f;
        }
        else
        {
            rigidbody2D.velocity = rigidbody2D.velocity + ((Vector2)(position - transform.position)).normalized * snapForce * 60 * Time.deltaTime;
        }
    }

    public void Relese()
    {
        if (ride != null)
        {
            ride.gravityScale = 1f;
        }
    }

}
