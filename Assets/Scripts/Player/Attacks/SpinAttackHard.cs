using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinAttackHard : MonoBehaviour, IAttack
{
    //public StickToPoint sticker;
    //public GameObject mountedObject;
    public float spinSpeed = 1f;
    public PlayerGameInput playerGameInput;
    public LayerMask groundLayer;
    //public AudioSource audioSource;
    DistanceJoint2D joint2D;

    Rigidbody2D rigidbody;
    bool ready = true;
    bool rotating = false;
    float currentDirection = 0f;
    [Range(0f,3f)]
    public float actionCD = 0.5f;

    public System.Action actionAfterAtk;
    public System.Action actionBeforeAtk;

    public virtual void attackBegin(float direction)
    {
        //Vector3 accDir = (mountedObject.transform.position - transform.position).normalized;
        //Vector3 motionDir = new Vector3(-1 * accDir.y, accDir.x, 0).normalized;
        //Debug.DrawLine(transform.position, motionDir + transform.position);
        //RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, 0.2f, -1 * direction * motionDir, 0.4f, groundLayer);
        //if (hit2D.collider == null)
        if (ready)
        {
            //audioSource.Play();
            FindObjectOfType<SFXManager>().PlayAudio("甩动", 0.5f);
            ready = false;
            currentDirection = direction;
            rotating = true;
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(Vector2.up * spinSpeed);
            Invoke("SetReady", actionCD);
        }
    }

    public virtual void LogicUpdate()
    {
        if(rotating)
        {
            
            //Debug.DrawLine(transform.position, CalMotionVec() * currentDirection + transform.position);
        }
    }

    public virtual void AttackEnd()
    {
        ready = true;
        rotating = false;
        Debug.Log("end");
    }

    private void SetReady()
    {
        this.ready = true;
    }


    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerGameInput.AtkAction.started += Attack;
    }

    //private Vector3 CalMotionVec()
    //{
    //    Vector3 accDir = (mountedObject.transform.position - transform.position).normalized;
    //    Vector3 motionDir = new Vector3(-1 * accDir.y, accDir.x, 0).normalized;
    //    return motionDir;
    //}

    private void Update()
    {
        LogicUpdate();
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        
            //Debug.Log("Atk");
            attackBegin(playerGameInput.AtkAction.ReadValue<float>());
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == groundLayer)
        {
            AttackEnd();
        }
    }

    private void fixRobotLength()
    {

    }

    private void OnDestroy()
    {
        playerGameInput.AtkAction.started -= Attack;
    }
}
