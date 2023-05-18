using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 inputVector;
    Rigidbody2D rigid;
    public float speed = 3f;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        inputVector.x = Input.GetAxisRaw("Horizontal");
        inputVector.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate() {
        Vector2 nextVector = inputVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
    }
}
