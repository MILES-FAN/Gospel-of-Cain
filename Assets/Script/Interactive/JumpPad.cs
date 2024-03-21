using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public enum TriggerType
    {
        Collider,Button
    }

    public enum JumpDirection
    {
        UP,DOWN,LEFT,RIGHT
    }



    bool released = false;
    public float resetTime = 1f;
    public float jumpHeight = 3f;
    public Sprite releasedSprite;
    Sprite defaultSprite;
    List<Rigidbody2D> rigidbodies;
    SpriteRenderer spriteRenderer;
    public TriggerType triggerType;
    public JumpDirection jumpDirection;

    private void Start()
    {
        rigidbodies = new List<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRenderer.sprite;
        if (releasedSprite == null)
        {
            releasedSprite = spriteRenderer.sprite;
        }
    }

    public void Triggered()
    {
        if(!released)
        {
            released = true;
            FindObjectOfType<SFXManager>().PlayAudio("弹簧", 0.8f);
            foreach(Rigidbody2D rigidbody in rigidbodies)
            {
                switch(jumpDirection)
                {
                    case JumpDirection.UP:
                        rigidbody.velocity = new Vector2(rigidbody.velocity.x, Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y * rigidbody.gravityScale) * jumpHeight));
                        break;
                    case JumpDirection.DOWN:
                        rigidbody.velocity = new Vector2(rigidbody.velocity.x, - Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y * rigidbody.gravityScale) * jumpHeight));
                        break;
                    case JumpDirection.LEFT:
                        rigidbody.velocity = new Vector2(-Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y * rigidbody.gravityScale) * jumpHeight), rigidbody.velocity.y);
                        break;
                    case JumpDirection.RIGHT:
                        rigidbody.velocity = new Vector2(Mathf.Sqrt(2 * Mathf.Abs(Physics2D.gravity.y * rigidbody.gravityScale) * jumpHeight), rigidbody.velocity.y);
                        break;
                }    

            }
            rigidbodies.Clear();
            spriteRenderer.sprite = releasedSprite;
            Invoke("resetPad", resetTime);
        }
    }

    public void resetPad()
    {
        released = false;
        spriteRenderer.sprite = defaultSprite;
    }

    private void RegCollision(Collision2D collision)
    {
        if (!rigidbodies.Contains(collision.rigidbody))
        {
            rigidbodies.Add(collision.rigidbody);
            if (triggerType == TriggerType.Collider)
            {
                Triggered();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RegCollision(collision);
    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        RegCollision(collision);
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(rigidbodies.Contains(collision.rigidbody))
        {
            rigidbodies.Remove(collision.rigidbody);
        }
    }

}
