using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed = 3f;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() 
    {
        Vector2 nextVector = inputVector * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
    }

    //프레임 넘어가기 전 함수
    void LateUpdate() 
    {
        //flipX 
        if (inputVector.x < 0) spriteRenderer.flipX = true;
        else if (0 < inputVector.x) spriteRenderer.flipX = false;

        //anim Run
        animator.SetFloat("Speed", inputVector.magnitude);
    }

    void OnMove(InputValue value) 
    {
        inputVector = value.Get<Vector2>();
    }
}
