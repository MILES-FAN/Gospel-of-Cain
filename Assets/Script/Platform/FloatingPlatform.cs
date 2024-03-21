using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FloatingPlatform : MonoBehaviour
{
    enum PlatformState {
        Forward, Backward
    }

    enum RepeatMethod
    {
        PingPong,Circling,None
    }

    [Header("路径点（无需添加初始点）")]
    public List<Transform> waypointTransforms;

    [HideInInspector] public List<Vector3> waypoints;

    [Header("自动开始")]
    public bool autoStart = false;
    //[Header("自动返回")]
    //public bool autoReturn = false;
    [Header("自动循环（需要自动返回开启）")]
    public bool loop = false;
    public float waitSecs = 0f;
    public float moveSpeed = 5f;
    public bool waiting = false;
    private int currentWaypointIndex = 0;
    private float waited = 0f;
    private Transform playerTransform;
    public bool started = false;
    private PlatformState state = PlatformState.Forward;
    public string[] audioNames = { "齿轮1", "齿轮2" };
    [Range(0f,1f)]
    public float volume = 0.3f;
    int currentAudioIndex = -1;
    [Header("循环方式")]
    [Tooltip("Ping-Pong为往返运动，Circling为循环，None关闭自动返回")]
    [SerializeField] private RepeatMethod repeatMethod = RepeatMethod.PingPong;


    private void Start()
    {
        if (waypointTransforms.Count == 0)
            waypointTransforms.Add(this.transform);
        this.waypoints = new List<Vector3>();
        this.waypoints.Add(transform.position);
        foreach (Transform transform in waypointTransforms)
        {
            this.waypoints.Add(transform.position);
        }
        currentWaypointIndex += 1;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform.parent;
        if (autoStart)
            StartPlatform();
    }

    private void Update()
    {
        MoveBF();
    }

    public void StartPlatform()
    {
        if(!started)
        {
            started = true;
            if(audioNames.Length!=0)
                currentAudioIndex = FindObjectOfType<SFXManager>().PlayAudioLooping(audioNames[Random.Range(0, audioNames.Length)],volume);
        }

    }


    public void ForceStopPlatform()
    {
        if(started)
        {
            started = false;
            FindObjectOfType<SFXManager>().StopAudioByIndex(currentAudioIndex);
        }
    }

    void MoveBF()
    {
        if (started)
        {
            if (waiting)
            {
                waited += Time.deltaTime;
                if (waitSecs < waited)
                    waiting = false;
            }
            else
            {
                waited = 0;
                float distance = Vector2.Distance(transform.position, this.waypoints[currentWaypointIndex]);
                switch (state)
                {
                    case PlatformState.Forward:
                        if (distance < 0.01f)
                        {
                            currentWaypointIndex += 1;
                            //Debug.Log(distance + " back " + currentWaypointIndex);
                            waiting = true;
                            if (currentWaypointIndex > this.waypoints.Count - 1)
                            {
                                waiting = false;
                                if (repeatMethod == RepeatMethod.PingPong)
                                {
                                    state = PlatformState.Backward;
                                    currentWaypointIndex = this.waypoints.Count - 1;
                                }
                                else if (repeatMethod == RepeatMethod.Circling)
                                    currentWaypointIndex = 0;
                                else
                                {
                                    currentWaypointIndex = this.waypoints.Count - 1;
                                    ForceStopPlatform();
                                }

                            }
                        }
                        break;
                    case PlatformState.Backward:
                        if (distance < 0.01f)
                        {
                            currentWaypointIndex -= 1;
                            //Debug.Log(distance + " forward " + currentWaypointIndex);
                            waiting = true;
                            if (currentWaypointIndex < 0)
                            {
                                waiting = false;
                                if (loop == false)
                                    ForceStopPlatform();
                                state = PlatformState.Forward;
                                currentWaypointIndex = 0;
                            }
                        }
                        break;
                    default:
                        break;
                }
                transform.position = Vector2.MoveTowards(transform.position, this.waypoints[currentWaypointIndex], moveSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //判断是否在平台上，玩家在平台上时变成平台的子类
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.transform.parent = gameObject.transform;
        }

    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        //离开平台
        if (collider.gameObject.CompareTag("Player"))
        {
            collider.gameObject.transform.parent = playerTransform;
        }
    }

}
