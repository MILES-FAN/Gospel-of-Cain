using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAction: ScriptableObject,IAttack
{
    bool ready = false;
    public float actionCD = 10f;

    public System.Action actionAfterAtk;
    public System.Action actionBeforeAtk;
    public virtual void Init(PlayerManager pm)
    {

    }
    public virtual void attackBegin(float direction)
    {
        actionBeforeAtk.Invoke();
        ready = false;
    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void AttackEnd()
    {
        actionAfterAtk.Invoke();
        ready = true;
    }
}
