using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;//무기개수 또는 관통력
    public float speed;

    float timer;
    Player player;

    void Awake() 
    {   
        //부모오브젝트의 컴포넌트 가져오기
        player = GetComponentInParent<Player>();
    }

    void Start()
    {
        Init();
    }

    void Update() 
    {
        switch (id)
        {
            case 0://(forward -> 0, 0, 1), (back -> 0, 0, -1)
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;

            case 1:
                timer += Time.deltaTime;

                if (timer > speed) {
                    timer = 0f;
                    Fire();
                }
                
                break;
            default:
                break;
        }

        //레벨업 예시
        if (Input.GetButtonDown("Jump")) LevelUp(damage + 10, ++count);
    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count = count;

        if (id == 0) Locate();
    }

    public void Init()
    {
        switch (id)
        {
            case 0:
                speed = 150;
                Locate();
                break;
            case 1:
                speed = 0.3f;
                break;
            default:
                break;
        }
    }

    //삽 배치
    void Locate()
    {
        for (int i = 0; i < count; i++) {
            //오브젝트 위치를 Weapon으로 변경하기 위해 transform가져옮
            Transform bullet;

            //원래 있던 근접 무기 재활용
            if (i < transform.childCount) {
                bullet = transform.GetChild(i);
            }
            else {//부족하면 생성
                bullet = GameManager.instance.pool.Get(prefabId).transform;

                //parent -> 부모오브젝트 변경
                bullet.parent = transform;
            }

            //초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            //회전
            Vector3 rotVec = Vector3.forward * 360 * i / count;
            bullet.Rotate(rotVec);
            //배치 -> local(자신)기준 up한것을 Space.World 기준으로 변환한다
            bullet.Translate(bullet.up * 1.2f, Space.World);

            //근접은 관통이 필요없어서 -1
            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);
        }
    }

    void Fire()
    {
        if (!player.scanner.nearestTarget) return ;

        //타겟방향 지정
        Vector3 targetPos = player.scanner.nearestTarget.position;
        Vector3 targetDir = (targetPos - transform.position).normalized;

        Transform bullet = GameManager.instance.pool.Get(2).transform;
        bullet.position = transform.position;

        //지정된 포지션 중심으로 목표를 향해 회전하는 함수 (벡터와 쿼터니언 배우기)
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, targetDir);

        bullet.GetComponent<Bullet>().Init(damage, count, targetDir);
    }
}
