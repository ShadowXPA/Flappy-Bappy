using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public int score;
    public bool gameOver;
    public GameObject gameOverScreen;
    public AudioSource gameAudio;
    public AudioSource checkpointAudio;
    public AudioSource gameOverAudio;
    private int highscore;

    void Start()
    {
        SetHighscore(PlayerPrefs.GetInt("highscore"));
    }

    public void AddScore(int scoreToAdd = 1)
    {
        if (!gameOver)
        {
            score += scoreToAdd;
            scoreText.text = $"{score}";
            checkpointAudio.Play();
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            gameOverScreen.SetActive(true);
            gameAudio.Stop();
            gameOverAudio.Play();

            if (score > highscore)
            {
                SetHighscore(score);
                PlayerPrefs.SetInt("highscore", highscore);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private void SetHighscore(int newHighscore)
    {
        highscore = newHighscore;
        highscoreText.text = $"Highscore: {highscore}";
    }
}
