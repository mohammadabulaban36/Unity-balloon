
using UnityEngine;
using UnityEngine.InputSystem;

// Handles balloon clicking/touching for both PC and Mobile.
public class BalloonRaycaster : MonoBehaviour
{
    Camera cam;

    void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        // =========================
        // PC - Mouse
        // =========================
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 screenPosition = Mouse.current.position.ReadValue();

            PopBalloonAtPosition(screenPosition);
        }

        // =========================
        // Mobile - Touch
        // =========================
        if (Touchscreen.current != null &&
            Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            Vector2 screenPosition =
                Touchscreen.current.primaryTouch.position.ReadValue();

            PopBalloonAtPosition(screenPosition);
        }
    }

    void PopBalloonAtPosition(Vector2 screenPosition)
    {
        // Convert screen position to world position
        Vector2 worldPosition = cam.ScreenToWorldPoint(screenPosition);

        // Check what was hit at that position
        RaycastHit2D hit =
            Physics2D.Raycast(worldPosition, Vector2.zero);

        // Nothing was hit
        if (hit.collider == null)
        {
            return;
        }

        // Check if the object is a balloon
        BalloonController balloon =
            hit.collider.GetComponent<BalloonController>();

        if (balloon != null)
        {
            balloon.Pop();
        }
    }
}