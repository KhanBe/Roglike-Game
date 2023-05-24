using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;

    float timer;

    void Awake() 
    {
        //자신 오브젝트를 포함 한 자식오브젝트의 여러개 컴포넌트 초기화
        spawnPoint = GetComponentsInChildren<Transform>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 0.5f) {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        //적 오브젝트 생성
        GameObject enemy = GameManager.instance.pool.Get(Random.Range(0, 2));

        //적 spawnPoint로 랜덤 배치
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}
