using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public float scanRange;
    public LayerMask targetLayer;//레이어 관리
    public RaycastHit2D[] targets;
    public Transform nearestTarget;

    void FixedUpdate() 
    {
        //CircleCastAll : 원형으로 캐스트 쏘고 결과를 모두 반환하는 함수
        //(캐스팅 시작 위치, 원의 반지름, 캐스팅 방향, 캐스팅 길이, 대상 레이어)
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetLayer);
        nearestTarget = GetNearest();
    }
    
    Transform GetNearest()
    {
        Transform result = null;
        float minDiff = 100;//최소값

        foreach (RaycastHit2D target in targets) {
            Vector3 playerPos = transform.position;
            Vector3 targetPos = target.transform.position;

            float diff = Vector3.Distance(playerPos, targetPos);

            if (diff < minDiff) {//최소길이target 업데이트
                result = target.transform;
                minDiff = diff;
            }
        }
        return result;
    }
}
