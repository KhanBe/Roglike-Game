using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;//관통

    Rigidbody2D rigid;

    void Awake() 
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    //데미지, 관통력, 목표방향
    public void Init(float damage, int per, Vector3 dir)
    {
        //this -> 해당클래스의 변수
        this.damage = damage;
        this.per = per;

        if (per > -1) {//속도 지정
            rigid.velocity = dir * 10f;
        }
    }

    void FixedUpdate()
    {
        Vector3 bulletPos = transform.position;
        Vector3 playerPos = GameManager.instance.player.transform.position;

        if ((bulletPos - playerPos).magnitude > 10) {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || per == -1) return;

        per--;

        if (per == -1) {
            rigid.velocity = Vector2.zero;
            gameObject.SetActive(false);
        }
    }
}
