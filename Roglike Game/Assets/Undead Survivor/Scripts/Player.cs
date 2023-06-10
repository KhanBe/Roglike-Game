using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    public float speed;
    public Scanner scanner;
    public Hand[] hands;
    //애니메이션컨트롤러 다루는 클래스
    public RuntimeAnimatorController[] animCon;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();

        //비활성화 된 오브젝트는 가져오지 못한다, (true 인자)비활성화된 오브젝트도 가져온다.
        hands = GetComponentsInChildren<Hand>(true);
    }

    void OnEnable()
    {
        speed *= Character.Speed;
        animator.runtimeAnimatorController = animCon[GameManager.instance.playerId];
    }

    void FixedUpdate() 
    {
        if (!GameManager.instance.isLive) return;

        Vector2 nextVector = inputVector * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
    }

    //프레임 넘어가기 전 함수
    void LateUpdate() 
    {
        if (!GameManager.instance.isLive) return;
        
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

    void OnCollisionStay2D(Collision2D other) 
    {
        if (!GameManager.instance.isLive) return;

        GameManager.instance.health -= Time.deltaTime * 10;

        if (GameManager.instance.health < 0) {//사망시 비활성화
            for (int i = 2; i < transform.childCount; i++) {//자식오브젝트 개수
                transform.GetChild(i).gameObject.SetActive(false);
            }

            //사망 애니
            animator.SetTrigger("Dead");
            GameManager.instance.GameOver();
        }
    }
}
