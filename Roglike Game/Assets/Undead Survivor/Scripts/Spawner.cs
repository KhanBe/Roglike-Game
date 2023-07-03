using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform[] spawnPoint;
    public SpawnData[] spawnData;
    public float levelTime;

    float timer;
    int level;

    void Awake() 
    {
        //자신 오브젝트를 포함 한 자식오브젝트의 여러개 컴포넌트 초기화
        spawnPoint = GetComponentsInChildren<Transform>();
        levelTime = GameManager.instance.maxGameTime / spawnData.Length;
    }

    void Update()
    {
        if (!GameManager.instance.isLive) return;
        
        timer += Time.deltaTime;
        
        //내림 후 int
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.gameTime / levelTime), spawnData.Length - 1);
        if (timer > spawnData[level].spawnTime) {
            timer = 0f;
            Spawn();
        }
    }

    void Spawn()
    {
        //적 오브젝트 생성
        GameObject enemy = GameManager.instance.pool.Get(0);

        //적 spawnPoint로 랜덤 배치
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        //Enemy클래스에서 만든 Init함수 호출
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

//직렬화
[System.Serializable]
public class SpawnData
{
    public float spawnTime;
    public int spriteType;
    public int health;
    public float speed;
}
