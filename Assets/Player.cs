using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private new SpriteRenderer renderer;
    private new Rigidbody2D rigidbody;
    
    private enum State { None, Walk, Jump } 
    private State state;

    private float applySpeed;
    public float moveSpeed;
    public float jumpSpeed;
    public float jumpHeight;

    private bool onGround;

    private void Awake()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        state = State.None;
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");

        float jumpForce = jumpHeight * 9.8f;

        if (onGround)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rigidbody.AddForce(Vector2.up * jumpForce);
                state = State.Jump;
            }
        }
        switch (state)
        {
            case State.None:
            case State.Walk:
                applySpeed = moveSpeed;
                break;
            case State.Jump:
                applySpeed = jumpSpeed; 
                break;
        }
        rigidbody.velocity = new Vector2(x * applySpeed, rigidbody.velocity.y);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        onGround = true;
        state = State.None;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        onGround = false;
    }

}
