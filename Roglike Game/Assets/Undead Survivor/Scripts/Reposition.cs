using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : MonoBehaviour
{
    //collider에 벗어나면
    void OnTriggerExit2D(Collider2D collision) 
    {
        if (collision.CompareTag("Area")) {//
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 myPos = transform.position;//재배치 할 포지션
            float diffX = Mathf.Abs(playerPos.x - myPos.x);
            float diffY = Mathf.Abs(playerPos.y - myPos.y);

            //playerDir:플레이어의 이동방향
            Vector3 playerDir = GameManager.instance.player.inputVector;
            float dirX = playerDir.x < 0 ? -1 : 1;
            float dirY = playerDir.y < 0 ? -1 : 1;

            //내 자신의 태그
            switch (transform.tag) {
                case "Ground":
                    if (diffX > diffY) {
                        //Translate : 지정된 값 만큼 현 위치에서 이동한다
                        //2칸을 뛰어넘어야하기때문에 40
                        transform.Translate(Vector3.right * dirX * 40);
                    }
                    else if (diffX < diffY) {
                        transform.Translate(Vector3.up * dirY * 40);
                    }
                    break;
                case "Enemy":
                    break;
            }
        }
    }
}