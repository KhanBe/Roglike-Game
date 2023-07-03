using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rect;
    Item[] items;

    public static int maxLevelItemCount;

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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }

    public void Select(int index)//기본무기
    {
        items[index].OnClick();
    }

    void Next()
    {
        foreach (Item item in items)
        {
            item.gameObject.SetActive(false);
        }

        //중복 없이 3개 랜덤 아이템 가져오기(힐템제외)
        int[] ran = new int[3];

        //뽑아야 할 갯수
        int remainItemCount = items.Length - maxLevelItemCount - 1;
        if (3 < remainItemCount) remainItemCount = 3;

        while (true)
        {
            int get = 0;

            for (int i = 0; i < ran.Length; i++) {
                ran[i] = Random.Range(0, items.Length - 1);

                if (!items[ran[i]].isMaxLevel) get++;
            }

            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2]) {
                if (remainItemCount == get) break;
            }
        }

        for (int i = 0; i < ran.Length; i++) {
            Item ranItem = items[ran[i]];

            if (!ranItem.isMaxLevel) ranItem.gameObject.SetActive(true);
            else items[items.Length - 1].gameObject.SetActive(true);//힐
        }

    }
}


