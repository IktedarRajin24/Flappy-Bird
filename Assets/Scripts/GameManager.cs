using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;
    private int highScore;

    [SerializeField] GameObject playBtn;
    [SerializeField] Player player;
    [SerializeField] PipeSpawner pipeSpawner;
    [SerializeField] GameObject gameOver;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    public static GameManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Pause();

        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public void Play()
    {
        AudioManager.instance.PlayAudio(Sound.ButtonClick);
        score = 0;
        scoreText.text = score.ToString();

        gameOver.SetActive(false);
        playBtn.SetActive(false);

        player.enabled = true;
        Time.timeScale = 1f;

        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
        playBtn.SetActive(true);

        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }
        highScoreText.text = "High score: " + highScore.ToString();

        Pause();
    }
    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
