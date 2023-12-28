using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private enum Direction
    {
        Left, 
        Right
    }

    public enum State
    {
        Idle,
        Move,
        Chase
    }

    private Player target;

    public State state;
    private new Rigidbody2D rigidbody;
    private Animator animator;
    private EnemySight sight;
    private Direction direction;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();    
        animator = GetComponent<Animator>();
        sight = GetComponentInChildren<EnemySight>();
    }

    private void OnEnable()
    {
        StartCoroutine(MovePatternCoroutine());
    }

    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                if (sight.targetExisted)
                {
                    target = sight.target;
                    state = State.Chase;
                    break;
                }
                break;
            case State.Move:
                if (sight.targetExisted)
                {
                    target = sight.target;
                    state = State.Chase;
                    break;
                }
                Move(direction, moveSpeed);
                break;
            case State.Chase:
                if (false == sight.targetExisted || target == null)
                {
                    target = null;
                    state = State.Move;
                    break;
                }
                Chase();
                break;
        }

        animator.SetInteger("State", (int)state);
    }

    private void Move(Direction dir, float speed)
    {
        transform.eulerAngles = (dir == Direction.Left) ? new Vector2(0, 180) : Vector2.zero;
        rigidbody.velocity = transform.right;
    }

    private void Chase()
    {
        Vector2 targetPos = target.transform.position;
        Vector2 dir = targetPos - (Vector2)transform.position;
        Direction direction = dir.x < 0 ? Direction.Left : Direction.Right;
        Move(direction, chaseSpeed);
    }

    private IEnumerator MovePatternCoroutine()
    {
        while (true)
        {
            //yield return new WaitUntil(() => state == State.Idle);
            float randomIdleTime = Random.Range(3, 8);
            yield return new WaitForSeconds(randomIdleTime);
            if (state == State.Idle)
                state = State.Move;
            else if (state == State.Chase) 
                continue;
            float randomMoveTime = Random.Range(8, 15);
            direction = (Direction)Random.Range(0, 2);
            yield return new WaitForSeconds(randomMoveTime / 2);
            direction = direction==Direction.Left ? Direction.Right : Direction.Left;
            yield return new WaitForSeconds(randomMoveTime / 2);
            if (state == State.Move)
                state = State.Idle;
        }
    }
}
