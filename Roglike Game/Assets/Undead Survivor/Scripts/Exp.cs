using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    public bool isDropExp;

    void Awake()
    {
        isDropExp = false;
    }

    void Update()
    {
        if (isDropExp) {
            Transform player = GameManager.instance.player.transform;

            //transform.position = Vector3.Lerp(transform.position, player.position, Time.deltaTime * 5f);
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 15f);
        }
    }

    void OnTriggerEnter2D (Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        GameManager.instance.GetExp();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Coin);

        gameObject.SetActive(false);
    }

    void OnDisable() 
    {
        isDropExp = false;
    }
}
