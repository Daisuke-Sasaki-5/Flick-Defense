using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    private bool isStarted = false; // ゲーム開始済みフラグ

    private float score;
    public bool IsStarted => isStarted;
    public bool isGameOver;
    
    private float startTiem;
    private float clearTime;

    private int enemyCount;

    [Header("スコアとボーナス")]
    [SerializeField] private int enemyScore = 100;
    [SerializeField] private int remainShotBonus = 500;

    [SerializeField] private int maxBaseHP = 5;
    private int currentBaseHP;

    [SerializeField] private TextMeshProUGUI countdownText;

    [SerializeField] private AudioClip gameBgm;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        currentBaseHP = maxBaseHP;
        instance = this;
    }

    private void Start()
    {
        Time.timeScale = 0f;

        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            StartCoroutine(WaitForFadeThenInit());
        }
    }

    private IEnumerator WaitForFadeThenInit()
    {
        // フェード完了を待つ
        if (FadeManager.instance != null)
        {
            while (!FadeManager.instance.IsFadeComplete) yield return null;
        }

        InitGame();

       yield return StartCoroutine(StartCountdown());
    }

    /// <summary>
    ///  ゲームの初期化
    /// </summary>
    private void InitGame()
    {
        // 現在のシーンをチェック
        if (SceneManager.GetActiveScene().name != "GameScene") return;

        isStarted = false;
        isGameOver= false;
        score = 0f;

        currentBaseHP = maxBaseHP;
        UIManager.Instance.UpdateUI(currentBaseHP);

        UIManager.Instance.ShowStartUI(true);

        // ゲーム停止中
        Time.timeScale = 0f;
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }

    private void StartGame()
    {
        isStarted = true;

        UIManager.Instance.ShowStartUI(false);

        Time.timeScale = 1f;

        startTiem = Time.time;

        FindObjectOfType<MobileInputVisualizer>().EnableInput();

        BGMManager.instance.PlayBGM(gameBgm);
    }

    public void TryClear()
    {
        Debug.Log("クリア");

        PlayerPrefs.SetInt("Score",GetScore());

        FadeManager.instance.FadeToScene("Result");
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // ==== シーンリセット ====
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameScene")
        {
            StartCoroutine(WaitForFadeThenInit());
        }
        else
        {
            Time.timeScale = 1;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void GameOver()
    {
        if(isGameOver)
        {
            return;
        }

        isGameOver = true;
        isStarted = false;

        Time.timeScale = 0f;
        UIManager.Instance.ShowGameOver(GetScore());
    }

    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void BackTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }

    public void OnEnemyDefeated()
    {
        score += enemyScore;
    }

    /// <summary>
    /// スコア加算
    /// </summary>
    /// <param name="value"></param>
    public void AddScore(int value)
    {
        score += value;
    }

    public void DamageBase(int damage)
    {
        currentBaseHP -= damage;

        UIManager.Instance.UpdateUI(currentBaseHP);

        if(currentBaseHP <= 0)
        {
            GameOver();
        }
    }
    
    public int GetCurrentHP()
    {
        return currentBaseHP;
    }

    private IEnumerator StartCountdown()
    {
        countdownText.gameObject.SetActive(true);

        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(0.5f);
        
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(0.5f);
        
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(0.5f);
        
        countdownText.text = "START";
        yield return new WaitForSecondsRealtime(0.5f);

        countdownText.gameObject.SetActive(false);

        StartGame();
    }
}
