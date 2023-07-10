using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    //프리펩들을 보관할 변수
    public GameObject[] prefabs;

    //풀 담당하는 리스트들 (배열안에 리스트)
    public List<GameObject>[] pools;

    void Awake() 
    {
        //초기화
        pools = new List<GameObject>[prefabs.Length]; 

        for (int i = 0; i < pools.Length; i++) {
            pools[i] = new List<GameObject>();
        }
    }

    //i -> prefabs의 종류 인덱스
    public GameObject Get(int i)
    {
        //반환할 오브젝트
        GameObject select = null;

        foreach (GameObject item in pools[i]) {
            if (!item.activeSelf) {//비활성화된 오브젝트이면
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select) { //null이면 생성
            //transform(자신) ->poolManager의 자식오브젝트로 들어간다
            select = Instantiate(prefabs[i], transform);
            
            pools[i].Add(select);
        }

        return select;
    }
}
