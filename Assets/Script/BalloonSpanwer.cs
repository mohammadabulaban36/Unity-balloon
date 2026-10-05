using UnityEngine;

// Keeps making balloons: a random colour, at a random spawn point, once a second.
public class BalloonSpanwer : MonoBehaviour
{

    [SerializeField] GameObject[] balloons;    // the seven prefabs
    [SerializeField] Transform[] balloonPos;   // the spawn point markers

    public void Initialize()
    {
        // Do not use InvokeRepeating("Spawn"), use nameof(Spawn).
        InvokeRepeating(nameof(Spawn), 1, 1f);
    }

    public void StopSpawning()
    {
        if (IsInvoking(nameof(Spawn)))
        {
            CancelInvoke(nameof(Spawn));
        }
    }

    void Spawn()
    {
        int randomBalloon = Random.Range(0, balloons.Length);
        int randomBalloonPos = Random.Range(0, balloonPos.Length);

        Instantiate(balloons[randomBalloon],
                    balloonPos[randomBalloonPos].position,
                    balloonPos[randomBalloonPos].rotation);
    }
}