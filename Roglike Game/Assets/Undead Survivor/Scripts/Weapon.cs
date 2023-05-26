using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;

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

            default:
                break;
        }
    }

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
            bullet.GetComponent<Bullet>().Init(damage, -1);
        }
    }
}
