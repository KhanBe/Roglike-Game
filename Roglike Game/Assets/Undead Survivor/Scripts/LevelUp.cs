using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        items = GetComponentsInChildren<Item>(true);//0~4까지 받음
    }

    public void Show()
    {
        Next();

        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }
    
    public void Select(int index)//기본무기
    {
        items[index].OnClick();
    }

    void Next()
    {
        foreach (Item item in items) {
            item.gameObject.SetActive(false);
        }

        //중복 없이 3개 랜덤 아이템 가져오기(힐템제외)
        int[] ran = new int[3];

        while (true) {
            ran[0] = Random.Range(0, items.Length - 1);
            ran[1] = Random.Range(0, items.Length - 1);
            ran[2] = Random.Range(0, items.Length - 1);

            if (ran[0] != ran[1] &&
                ran[1] != ran[2] &&
                ran[0] != ran[2]) break;
        }

        for (int i = 0; i < ran.Length; i++) {
            Item ranItem = items[ran[i]];

            //만렙일 경우 4-> 소비(힐템)
            if (ranItem.level == ranItem.data.damages.Length) {
                items[4].gameObject.SetActive(true);
            }
            else {
                ranItem.gameObject.SetActive(true);
            }
        }
    }
}
