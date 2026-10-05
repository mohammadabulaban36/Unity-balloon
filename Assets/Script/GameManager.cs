using UnityEngine;

// The boss. Starts the game, reacts to a pop, counts misses, and ends the game.
public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    [SerializeField] SoundControoller soundControoller;
    [SerializeField] UIController uiController;
    [SerializeField] BalloonSpanwer balloonSpanwer;

    [SerializeField] int maxMisses = 5;

    int misses;
    bool gameOver;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        balloonSpanwer.Initialize();
    }

    // A balloon was popped by the player.
    public void DestroyBalloon()
    {
        if (gameOver)
        {
            return;
        }

        soundControoller.PlayDestroyBalloonEffect();
        
    }
    // A balloon floated off the top without being popped.
    public void BalloonEscaped()
    {
        if (gameOver)
        {
            return;
        }

        misses++;
                uiController.heart.text = "❤" + (maxMisses - misses).ToString();
        if (misses >= maxMisses)
        {
            GameOver();
        }
    }
    public bool IsGameOver()
    {
        return gameOver;
    }
    public void GameOver()
    {
        gameOver = true;
        balloonSpanwer.StopSpawning();
        uiController.ShowGameOver();
    }
}