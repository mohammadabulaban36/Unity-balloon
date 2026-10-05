using UnityEngine;
using UnityEngine.SceneManagement;   // for reloading the scene
using TMPro;

// Everything the player sees on the Canvas: the score, and the game over panel.
public class UIController : MonoBehaviour
{

    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverScoreText;
    [SerializeField] public TMP_Text heart;
    int score;

    void Awake()
    {
        // Always start hidden, however it was left in the Editor.
        gameOverPanel.SetActive(false);
    }

    public void AddScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
        if(score < 0)
        {
           GameManager.instance.GameOver();
        }
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = score.ToString();
    }

    // Wired to the Retry button's On Click () list in the Inspector.
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}