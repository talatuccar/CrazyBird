using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private Button restartButton;

    [Header("Score Settings")]
    [SerializeField] private TextMeshProUGUI hisghestScoreText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    [Header("Audio Settings")]
    public AudioSource audioSource;
    public AudioClip passSoundEffect;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        PlayerPrefCheck();
    }
    private void Start()
    {
        restartButton.onClick.RemoveAllListeners();
        restartButton.onClick.AddListener(RestartGame);
    }
    public void IncreaseScore()
    {
        
        audioSource.PlayOneShot(passSoundEffect);
        score++;
        scoreText.text = score.ToString();
    }
    public void ScoreCheck()
    {
        if (score > PlayerPrefs.GetInt("highest"))
        {
            PlayerPrefs.SetInt("highest", score);
            PlayerPrefs.Save();
        }
        else
        {
            return;
        }
    }
    public void GameOver()
    {
        ScoreCheck();
        hisghestScoreText.text = PlayerPrefs.GetInt("highest").ToString();

        if (gameOverPanel != null) // Null kontrolü
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
    void PlayerPrefCheck()
    {
        if (!PlayerPrefs.HasKey("highest"))
        {
            PlayerPrefs.SetInt("highest", 0);
        }
    }
    void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
