using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;//관통

    public void Init(float damage, int per)
    {
        //this -> 해당클래스의 변수
        this.damage = damage;
        this.per = per;
    }
}
