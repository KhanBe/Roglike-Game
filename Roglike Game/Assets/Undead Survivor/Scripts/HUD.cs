using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public enum InfoType { Exp, Level, Kill, Time, Health }
    public InfoType type;

    Text myText;
    Slider mySlider;

    void Awake() 
    {
        myText = GetComponent<Text>();
        mySlider = GetComponent<Slider>();
    }

    //Update에서 데이터 바뀐 후 정보전달용
    void LateUpdate() 
    {
        switch (type) {
            case InfoType.Exp:
                float curExp = GameManager.instance.exp;
                //최대 레벨일 경우 마지막 경험치만
                float maxExp = GameManager.instance.nextExp[Mathf.Min(GameManager.instance.level, GameManager.instance.nextExp.Length - 1)];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = "Lv." + GameManager.instance.level;
                break;
            case InfoType.Kill:
                myText.text = "" + GameManager.instance.kill;
                break;
            case InfoType.Time:
                float remainTime = GameManager.instance.maxGameTime - GameManager.instance.gameTime;
                int min = Mathf.FloorToInt(remainTime / 60);
                int sec = Mathf.FloorToInt(remainTime % 60);
                myText.text = min + ":" + sec;
                break;
            case InfoType.Health:
                float curHealth = GameManager.instance.health;
                float maxHealth = GameManager.instance.maxHealth;
                mySlider.value = curHealth / maxHealth;
                break;
        }
    }
}
