using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    public enum State 
    {
        Idle    = 0,
        Walk    = 1,
        Jump    = 2, 
        Sit     = 3, 
    }

    private State state;

    private new Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private bool isGrounded;
    private bool onPlat;
    private bool isWalking;

    public float moveSpeed; 
    public float jumpPower; 

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (Input.GetAxisRaw("Horizontal") != 0)
                {
                    state = State.Walk;
                }
                
                if (Input.GetButtonDown("Jump"))
                {
                    rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    state = State.Jump;
                }
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    state = State.Sit;
                }
                break;
            case State.Walk:
                float x = Input.GetAxisRaw("Horizontal");
                if (x == 0)
                {
                    state = State.Idle;
                    break;
                }
                isWalking = Mathf.Abs(x) > 0;
                if (x < 0) spriteRenderer.flipX = true;
                if (x > 0) spriteRenderer.flipX = false;
                animator.SetFloat("MoveSpeed", moveSpeed);
                rigidbody.velocity = new Vector2(x * moveSpeed, rigidbody.velocity.y);

                if (Input.GetButtonDown("Jump"))
                {
                    rigidbody.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
                    state = State.Jump;
                }
                if (Input.GetAxisRaw("Vertical") < 0)
                {
                    state = State.Sit;
                }
                break;
            case State.Jump:
                if (isGrounded)
                {
                    state = State.Idle;
                }
                break;
            case State.Sit:
                if (Input.GetAxisRaw("Vertical") >= 0) 
                {
                    state = State.Idle;
                }
                if (Input.GetButtonDown("Jump") && onPlat)
                {
                    StartCoroutine(ThroughingCoroutine());
                }
                break;
        }
        animator.SetInteger("State", (int)state);
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        if (collision.collider.tag == "Plat")
        {
            onPlat = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
        onPlat = false;
    }

    private IEnumerator ThroughingCoroutine()
    {
        gameObject.layer = LayerMask.NameToLayer("Throughing");
        yield return new WaitForSeconds(0.2f);
        gameObject.layer = LayerMask.NameToLayer("Normal");
    }
}
