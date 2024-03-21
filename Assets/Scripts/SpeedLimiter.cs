using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedLimiter : MonoBehaviour
{
    [SerializeField] PlayerManager pm;
    Rigidbody2D rigidbody;
    [HideInInspector] public bool limited = true;
    // Start is called before the first frame update
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if(limited)
            rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -pm._moveClamp, pm._moveClamp), rigidbody.velocity.y);
    }
}
