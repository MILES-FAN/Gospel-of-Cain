using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Magnet : MonoBehaviour
{
    public enum MagnetType
    {
        hook,item
    }
    public float force = 4f;
    [Range(0f,5f)]
    public float fixDistance = 1f;
    [SerializeField]
    public MagnetType magnetType;
    TrackControllable controllable;

    bool snapped = false;

    [Tooltip("吸附时发生")]
    public UnityEvent unityEvent;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Attract(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Attract(collision);
    }
    void Attract(Collider2D collision)
    {
        //Debug.Log(collision.gameObject.CompareTag("Robot"));
        MagnetSnap robot;
        if (collision.gameObject.CompareTag("Robot") && collision.gameObject.TryGetComponent<MagnetSnap>(out robot))
        {
            float objectDistance = Vector2.Distance(transform.position, collision.transform.position);

            if(robot.snapping)
            {
                if(objectDistance < fixDistance && magnetType == MagnetType.hook)
                {
                    if(!snapped)
                    {
                        unityEvent.Invoke();
                    }
                    snapped = true;
                    robot.FixToMagnet(this.transform);
                }
                else
                {
                    snapped = false;
                }
                if (magnetType == MagnetType.item)
                {
                    if(controllable == null)
                    {
                        controllable = GetComponent<TrackControllable>();
                    }
                    controllable.Snap(robot.gameObject.transform.position,force);
                }
                collision.GetComponent<Rigidbody2D>().AddForce((transform.position - collision.transform.position).normalized * force / (objectDistance + 0.01f) * 120 * Time.deltaTime);
                Debug.DrawLine(transform.position, collision.transform.position, Color.red);
            }
        }
    }
}
