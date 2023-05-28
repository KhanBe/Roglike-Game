using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //static으로 메모리에 할당
    public static GameManager instance;

    public float gameTime;
    public float maxGameTime = 2 * 10f;

    public Player player;
    public PoolManager pool;
    
    void Awake()
    {
        instance = this;//자기 자신 값 넣기
    }

    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime){
            gameTime = maxGameTime;
        }
    }
}
