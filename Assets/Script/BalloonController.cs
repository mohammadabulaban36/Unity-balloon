using UnityEngine;
using UnityEngine.InputSystem.XR;

// One balloon. It floats up, and it dies in one of two ways:
//   1. the player pops it  (BalloonRaycaster calls Pop)
//   2. it escapes off the top of the screen
public class BalloonController : MonoBehaviour
{
    UIController uiController;
    public float speed;

    // How long the pop animation lasts. We wait this long before removing
    // the balloon, so the player actually sees it burst.
    [SerializeField] float popDuration = 0.2f;

    // Just above the top of the screen. Past this line the balloon has escaped.
    [SerializeField] float escapeHeight = 7f;

    Animator anim;    // the Animator component sitting next to us
    bool popped;      // a balloon can only be popped once

    void Awake()
    {
        anim = GetComponent<Animator>();
        uiController = FindObjectOfType<UIController>();
        speed = Random.Range(1f, 5f);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Gone off the top? It got away.
        if (transform.position.y > escapeHeight)
        {
            // Only count it as a miss if the player never popped it. A balloon popped
            // near the top can still drift past this line while it is bursting.
            if (!popped)
            {
                GameManager.instance.BalloonEscaped();
            }

            Destroy(gameObject);
        }
    }
    // Called by BalloonRaycaster when the player's click ray hits this balloon.
    public void Pop()
    {
        if (popped)
        {
            return;
        }

        popped = true;

        if (!GameManager.instance.IsGameOver())
        {
            if (CompareTag("green"))
            {
                uiController.AddScore(5);
            }
            else if (CompareTag("yellow"))
            {
                uiController.AddScore(-3);
            }
            else
            {
                uiController.AddScore(1);
            }

            GameManager.instance.DestroyBalloon();
        }

        anim.SetTrigger("Destroy");
        Destroy(gameObject, popDuration);
    }
}