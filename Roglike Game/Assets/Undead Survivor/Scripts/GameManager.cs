using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //static으로 메모리에 할당
    public static GameManager instance;
    [Header("# Game Control")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;
    [Header("# Player Info")]
    public int playerId;
    public int level;
    public int kill;
    public int exp;
    public float health;
    public float maxHealth = 100f;
    public int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450, 600};
    [Header("# Game Object")]
    public Player player;
    public PoolManager pool;
    public LevelUp LevelUpUi;
    public Result uiResult;
    public GameObject enemyCleaner;
    public Transform uiJoy;

    void Awake()
    {
        instance = this;//자기 자신 값 넣기
    }

    public void GameStart(int id) 
    {
        playerId = id;//고유번호
        health = maxHealth;//체력
        player.gameObject.SetActive(true);//플레이어
        LevelUpUi.Select(playerId % 2);//무기

        enemyCleaner.gameObject.SetActive(false);
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;

        //묘비 애니 기다리기
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();
        
        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameVictory()
    {
        StartCoroutine(GameVictoryRoutine());
    }

    IEnumerator GameVictoryRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        //적 죽이는거 기다리기
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        //인자 : string 또는 index
        SceneManager.LoadScene(0);
        Resume();
    }

    void Update()
    {
        if (!isLive) return;

        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime){//시간 지나면
            gameTime = maxGameTime;
            GameVictory();
        }
    }

    public void GetExp()
    {
        if (!isLive) return;

        exp++;

        //최대 레벨일 경우 마지막 경험치만
        if (exp == nextExp[Mathf.Min(level, nextExp.Length - 1)]) {//레벨업 시
            level++;
            exp = 0;
            LevelUpUi.Show();
        }
    }

    public void Stop()
    {
        isLive = false;

        //tiemScale = 시간이 흐르는 속도
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
    }

    public void Resume()
    {
        isLive = true;

        //tiemScale = 시간이 흐르는 속도
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
    }
}
