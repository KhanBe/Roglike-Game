using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    void Awake() 
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate() 
    {
        //월드좌표를 스크린좌표로 변환
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
    }
}
