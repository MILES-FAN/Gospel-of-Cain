using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

[RequireComponent(typeof(PathCreator))]
public class TrackRide : MonoBehaviour
{
    public enum StartedFrom
    {
        Terminus, Midway
    }

    PathCreator pathCreator;
    public Rigidbody2D objectRigidbody;
    public Vector2 velocity;
    [Range(0f,1f)]
    public float fraction = 0f;
    bool moving = false;
    float currentDistance;
    Vector2 rawPos;
    float exitDistance;
    TrackEntrance[] entrances;
    bool midwayStarted = false;
    bool noexit = false;
    internal float gravityScale = 1f;

    private void Start()
    {
        pathCreator = GetComponent<PathCreator>();
        entrances = GetComponentsInChildren<TrackEntrance>();
        if (entrances.Length == 2)
            CalibrateEntrance();
        else
        {
            noexit = true;
        }
        if (objectRigidbody!=null)
        {
            StartMovingFrom(objectRigidbody.gameObject, StartedFrom.Midway);
        }
    }
    public void StartMoving(GameObject toMove)
    {
        StartMovingFrom(toMove, StartedFrom.Terminus);
    }


    public void StartMovingFrom(GameObject toMove ,StartedFrom from)
    {
        moving = true;
        toMove.GetComponent<TrackControllable>().ride = this;
        objectRigidbody = toMove.GetComponent<Rigidbody2D>();
        velocity = objectRigidbody.velocity;
        objectRigidbody.bodyType = RigidbodyType2D.Kinematic;
        //objectRigidbody.bodyType = RigidbodyType2D.Static;
        rawPos = objectRigidbody.gameObject.transform.position;
        if(from == StartedFrom.Terminus)
        {
            exitDistance = Mathf.Abs(pathCreator.path.length - pathCreator.path.GetClosestDistanceAlongPath(rawPos));
            //Debug.Log(exitDistance);
            midwayStarted = false;
            Invoke("SetMidway", 2f);
        }
        else
        {
            midwayStarted = true;
        }
        disableAllColliders();
    }

    private void SetMidway()
    {
        midwayStarted = true;
    }

    private void disableAllColliders()
    {
        foreach (TrackEntrance entrance in entrances)
        {
            entrance.gameObject.SetActive(false);
        }
    }

    private void enableAllColliders()
    {
        foreach (TrackEntrance entrance in entrances)
        {
            entrance.gameObject.SetActive(true);
        }
    }

    public void EndMoving()
    {

        moving = false;
        objectRigidbody.gameObject.GetComponent<TrackControllable>().ride = null;
        objectRigidbody.bodyType = RigidbodyType2D.Dynamic;
        objectRigidbody.velocity = velocity;
        Invoke("enableAllColliders", 2f);
        
    }

    public void CalibrateEntrance()
    {
        pathCreator = GetComponent<PathCreator>();
        entrances = GetComponentsInChildren<TrackEntrance>();
        if(entrances.Length == 2)
        {
            entrances[0].CalibrateThis(pathCreator.path.GetPointAtDistance(0));
            entrances[1].CalibrateThis(pathCreator.path.GetPointAtDistance(pathCreator.path.length, EndOfPathInstruction.Stop));
        }
        else
        {
            Debug.LogError("没有两个入口");
        }
    }

    private void Update()
    {
        if(moving)
        {
            currentDistance = pathCreator.path.GetClosestDistanceAlongPath(rawPos);
            if(!noexit)
            {
                if (midwayStarted && pathCreator.path.GetPointAtDistance(currentDistance, EndOfPathInstruction.Stop) != pathCreator.path.GetPointAtDistance(currentDistance, EndOfPathInstruction.Loop))
                {
                    EndMoving();
                    return;
                }
                else if ((currentDistance - exitDistance) < 0.1f)
                {
                    EndMoving();
                    return;
                }
            }
            float velocityX = 0f;
            if(velocity.x > 0)
            {
                velocityX = Mathf.Clamp(velocity.x - 10 * fraction * 9.8f * Time.deltaTime,0f,999f);
            }
            else
            {
                velocityX = Mathf.Clamp(velocity.x + 10 * fraction * 9.8f * Time.deltaTime, -999f, 0f);
            }
            velocity = new Vector2(velocityX, velocity.y - 9.8f * Time.deltaTime *(1f - fraction) * gravityScale);
            //Vector2 normal = pathCreator.path.GetNormalAtDistance(currentDistance, EndOfPathInstruction.Stop).normalized;
            Vector2 direction = pathCreator.path.GetDirectionAtDistance(currentDistance, EndOfPathInstruction.Stop).normalized;
            //Debug.DrawLine(objectRigidbody.transform.position, objectRigidbody.transform.position + (Vector3)direction);
            float angle = Vector2.Angle(velocity, direction);
            float speed = velocity.magnitude * Mathf.Cos(angle * Mathf.Deg2Rad);
            //velocity = speed * direction;
            if (angle < 90f)
                velocity = velocity.magnitude * direction;
            else
                velocity = velocity.magnitude * direction * -1f;
            //Debug.Log(string.Format("V:{0},S:{1},A:{2}\nSinA*S:{3}",velocity,velocity.magnitude,angle,speed));
            rawPos = pathCreator.path.GetPointAtDistance(currentDistance,EndOfPathInstruction.Stop) + (Vector3)velocity * Time.deltaTime;
            objectRigidbody.gameObject.transform.position = Vector3.Lerp(objectRigidbody.gameObject.transform.position, rawPos, 0.5f);
        }
    }
}
