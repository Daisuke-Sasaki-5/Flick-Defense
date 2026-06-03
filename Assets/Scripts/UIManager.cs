using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{ 
    public static UIManager Instance;

    [Header("GameOver UI")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI resultScoreText;

    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("ÉnÅ[ÉgUI")]
    [SerializeField] private Image[] haerts;
    [SerializeField] private Sprite fullHertt;
    [SerializeField] private Sprite emptyHeart;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        gameOverPanel.SetActive(false);

        scoreText.gameObject.SetActive(false);

        UpdateUI(GameManager.instance.GetCurrentHP());
    }

    private void Update()
    {
        if (GameManager.instance.IsStarted)
        {
            scoreText.text = "Score:" + GameManager.instance.GetScore();
        }
    }

    // ==== Start UI ====
    public void ShowStartUI(bool show)
    {
        scoreText?.gameObject.SetActive(!show);
    }

    // ==== GameOver UI ====
    public void ShowGameOver(int score)
    {
        gameOverPanel.SetActive(true);
        scoreText.gameObject.SetActive(false);

        resultScoreText.text = "Score:" + score;
    }

    public void UpdateUI(int currentHP)
    {
        for(int i = 0; i < haerts.Length; i++)
        {
            haerts[i].sprite = i < currentHP ? fullHertt : emptyHeart;
        }
    }
}
