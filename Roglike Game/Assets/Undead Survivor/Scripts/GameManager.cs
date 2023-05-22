using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //static으로 메모리에 할당
    public static GameManager instance;
    public Player player;

    void Awake()
    {
        instance = this;//자기 자신 값 넣기
    }
}
