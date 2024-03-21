using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackEntrance : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("cart")&&!collision.isTrigger)
            this.GetComponentInParent<TrackRide>().StartMoving(collision.gameObject);
    }

    public void CalibrateThis(Vector3 pos)
    {
        transform.position = pos;
    }
}
