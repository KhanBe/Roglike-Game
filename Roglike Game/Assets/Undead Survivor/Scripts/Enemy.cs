using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    Collider2D coll;

    //애니메이터컨트롤러 관리 변수
    public RuntimeAnimatorController[] animCon;
    public Rigidbody2D target;

    //다음 fixedUpdate가 될때까지 기다린다
    WaitForFixedUpdate wait;

    bool isLive;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();

        wait = new WaitForFixedUpdate();
        
    }

    //물리적 이동에는 FixedUpdate()
    void FixedUpdate()
    {
        if (!GameManager.instance.isLive) return;

        //hit 애니일때 넉백을 주기 위해 물리이동 필터
        //인자 0 -> animator의 Base Layer값이 된다.
        if (!isLive || animator.GetCurrentAnimatorStateInfo(0).IsName("Hit")) return;

        //dirVec에 normalized하면 방향이 나오게 된다.
        Vector2 dirVec = target.position - rigid.position;
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
        //플레이어와 적이 물리 충돌로 얻어지는 속도를 0으로
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate() 
    {
        if (!GameManager.instance.isLive) return;
        if (!isLive) return;

        //flipX
        spriter.flipX = target.position.x < rigid.position.x;
    }

    //활성화 되었을 때
    void OnEnable() 
    {
        //Enemy의 타겟 설정
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;//order in layer
        animator.SetBool("Dead", false);
        health = maxHealth;
    }

    //직렬화 데이터를 받는 함수
    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animCon[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    void OnTriggerEnter2D(Collider2D collision) 
    {
        if (!collision.CompareTag("Bullet") || !isLive) return;

        health -= collision.GetComponent<Bullet>().damage;
        StartCoroutine(KnockBack());

        if (health > 0) {
            animator.SetTrigger("Hit");
        }
        else {
            //비활성화
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;//order in layer
            animator.SetBool("Dead", true);
            //Dead();animation ->add event로 1초뒤 함수 불러 옮

            //경험치 떨구기
            GameObject exp = GameManager.instance.pool.Get(3);
            exp.transform.position = transform.position;

            GameManager.instance.kill++;
        }
    }

    //코루틴
    IEnumerator KnockBack()
    {
        yield return wait;
        Vector3 playerPos = GameManager.instance.player.transform.position;
        Vector3 knockBackdir = transform.position - playerPos;

        rigid.AddForce(knockBackdir.normalized * 3, ForceMode2D.Impulse);
    }

    void Dead() 
    {
        gameObject.SetActive(false);
    }
}
