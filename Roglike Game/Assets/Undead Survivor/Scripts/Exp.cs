using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp : MonoBehaviour
{
    void OnTriggerEnter2D (Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        GameManager.instance.GetExp();

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Coin);

        gameObject.SetActive(false);
    }
}
