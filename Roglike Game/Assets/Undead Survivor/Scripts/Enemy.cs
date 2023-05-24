using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Rigidbody2D target;

    bool isLive = true;
    Rigidbody2D rigid;
    SpriteRenderer spriter;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
    }

    //물리적 이동에는 FixedUpdate()
    void FixedUpdate()
    {
        if (!isLive) return;

        //dirVec에 normalized하면 방향이 나오게 된다.
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        //플레이어와 적이 물리 충돌로 얻어지는 속도를 0으로
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate() 
    {
        if (!isLive) return;

        //flipX
        spriter.flipX = target.position.x < rigid.position.x;
    }

    //활성화 되었을 때
    void OnEnable() 
    {
        //Enemy의 타겟 설정
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
    }
}
