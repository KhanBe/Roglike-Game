using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unLockCharacter;
    public GameObject uiNotice;

    enum Achive { UnlockBean, UnlockOrange }
    Achive[] achives;   
    WaitForSecondsRealtime wait;//현실시간//timeScale에 영향 안줌

    void Awake() 
    {
        //enum값을 배열에 넣는 방법
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        wait = new WaitForSecondsRealtime(3);
        if (!PlayerPrefs.HasKey("MyData")) Init();
    }

    void Init()
    {
        //각 디바이스에 데이터 저장 클래스 (key, value)   
        PlayerPrefs.SetInt("MyData", 1);
        
        foreach (Achive achive in achives) {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    void Start() 
    {
        UnlockCharacter();
    }

    void UnlockCharacter()
    {
        for (int i = 0; i < lockCharacter.Length; i++) {
            string achiveName = achives[i].ToString();
            bool isUnLock = PlayerPrefs.GetInt(achiveName) == 1;
            //해금 시 비활성화
            lockCharacter[i].SetActive(!isUnLock);
            unLockCharacter[i].SetActive(isUnLock);
        }
    }

    void LateUpdate() 
    {
        foreach (Achive achive in achives) {
            CheckAchive(achive);
        }
    }

    void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive) {
            case Achive.UnlockBean:
                isAchive = GameManager.instance.kill >= 10;
                break;
            case Achive.UnlockOrange:
                isAchive = GameManager.instance.gameTime == GameManager.instance.maxGameTime;
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0) {//업적 달성 시
            PlayerPrefs.SetInt(achive.ToString(), 1);

            for (int i = 0; i < uiNotice.transform.childCount; i++) {
                bool isActive = (i == (int)achive);
                uiNotice.transform.GetChild(i).gameObject.SetActive(isActive);
            }
            StartCoroutine(NoticeRoutine());
        }
    }

    IEnumerator NoticeRoutine()
    {
        uiNotice.SetActive(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.LevelUp);
        
        yield return wait;//최적화

        uiNotice.SetActive(false);
    }
}
