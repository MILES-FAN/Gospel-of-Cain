using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpinAttack : MonoBehaviour, IAttack
{
    public StickToPoint sticker;
    public GameObject mountedObject;
    public float spinDuration = 1f;
    public PlayerGameInput playerGameInput;
    public LayerMask groundLayer;

    bool ready = true;
    public float actionCD = 10f;

    public System.Action actionAfterAtk;
    public System.Action actionBeforeAtk;

    public virtual void attackBegin(float direction)
    {
        sticker.Connect();
        Vector3 accDir = (mountedObject.transform.position - transform.position).normalized;
        Vector3 motionDir = new Vector3(-1 * accDir.y, accDir.x, 0).normalized;
        Debug.DrawLine(transform.position, motionDir + transform.position);
        RaycastHit2D hit2D = Physics2D.CircleCast(transform.position, 0.2f, -1 * direction * motionDir, 0.4f, groundLayer);
        if (hit2D.collider == null)
        {
            ready = false;
            LeanTween.rotateAround(mountedObject, Vector3.forward * direction, 360, spinDuration).setOnComplete(AttackEnd);
        }
        else
        {
            sticker.Disconnect();
        }

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void AttackEnd()
    {
        ready = true;
        sticker.Disconnect();
        Debug.Log("end");
    }


    void Start()
    {
        playerGameInput.AtkAction.started += Attack;
    }

    private void Update()
    {
        Vector3 accDir = (mountedObject.transform.position - transform.position).normalized;
        Vector3 motionDir = new Vector3(-1 * accDir.y, accDir.x, 0).normalized;
        Debug.DrawLine(transform.position, motionDir + transform.position);
    }

    public void Attack(InputAction.CallbackContext callback)
    {
        if(ready && mountedObject != null)
        {
            //Debug.Log("Atk");
            attackBegin(playerGameInput.AtkAction.ReadValue<float>());
        }
    }

}
