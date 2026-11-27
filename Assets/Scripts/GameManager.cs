using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int score = 0;
    int bestScore = 0;

    public bool gameStarted = false;
    public bool gameOver = false;

    [Header("UI")]
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject newRecordPanel;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI newRecordText;

    [Header("Objectes")]
    public GameObject pipeSpawner;
    public GameObject player;

    [Header("Sons")]
    public AudioClip flapSound;
    public AudioClip hitSound;
    public AudioClip startSound;
    public AudioClip newRecordSound;
    public AudioClip pointSound;
    private AudioSource audioSource;

    void Start()
    {
        Application.targetFrameRate = 60;

        audioSource = GetComponent<AudioSource>();

        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        newRecordPanel.SetActive(false);
        UpdateScoreInfo();
    }

    void UpdateScoreInfo()
    {
        scoreText.SetText("Score: " + score);
        bestScoreText.SetText("Best: " + bestScore);
    }

    public void StartGame()
    {
        if (gameStarted) return;

        gameStarted = true;
        startPanel.SetActive(false);
        PlaySound(startSound, 0.7f);

        pipeSpawner.GetComponent<PipeSpawner>().StartSpawning();
        player.GetComponent<Player>().StartFlying();
    }

    public void AddScore(int amount)
    {
        if (gameOver) return;

        score += amount;
        UpdateScoreInfo();
        PlaySound(pointSound, 0.6f);
    }

    public void StopGame()
    {
        if (gameOver) return;

        gameOver = true;
        Debug.Log("Game stopped");
        PlaySound(hitSound, 0.9f);
        gameOverPanel.SetActive(true);

        player.GetComponent<Player>().Die();
        pipeSpawner.GetComponent<PipeSpawner>().StopSpawning();

        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore", bestScore);
            PlayerPrefs.Save();
            UpdateScoreInfo();
            Invoke("ShowNewRecord", 0.6f);
        }
    }

    void ShowNewRecord()
    {
        newRecordText.SetText("NEW RECORD!\n" + score);
        newRecordPanel.SetActive(true);
        PlaySound(newRecordSound, 0.8f);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        audioSource.PlayOneShot(clip, volume);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
