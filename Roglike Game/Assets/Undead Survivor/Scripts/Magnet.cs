using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    WaitForSeconds dropTime = new WaitForSeconds(0.1f);
    
    bool isDropping;
    float dropSpeed = 5f;

    void Awake()
    {
        StartCoroutine(Drop());
    }

    void Update()
    {
        if (!isDropping) return;

        Vector3 dir = (GameManager.instance.player.transform.position - transform.position).normalized;
        
        transform.position += dir * Time.deltaTime * dropSpeed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        //효과음
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Magnet);

        //exp 
        List<GameObject> expPool = GameManager.instance.pool.pools[3];
        //필드에 떨어진 exp만 isDropExp = true
        foreach (GameObject exp in expPool) {
            exp.GetComponent<Exp>().isDropExp = true;
        }
        gameObject.SetActive(false);
    }

    //자석
    IEnumerator Drop()
    {
        isDropping = true;
        yield return dropTime;
        isDropping = false;
    }
}
