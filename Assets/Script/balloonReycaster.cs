using UnityEngine;
using UnityEngine.InputSystem;   // the modern Input System, for reading the mouse

// Turns a mouse click into a 2D ray, and pops whatever balloon that ray hits.
//
// This script lives on the Main Camera, because only the camera knows how to
// turn a point on your screen (pixels) into a point in the game world (units).
public class BalloonRaycaster : MonoBehaviour
{

    Camera cam;

    void Awake()
    {
        cam = Camera.main;   // "the camera tagged MainCamera" — that's ours
    }

    void Update()
    {
        // No mouse attached? Nothing to do.
        if (Mouse.current == null)
        {
            return;
        }

        // Only react on the single frame the left button goes DOWN,
        // otherwise we would fire every frame while the button is held.
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 1. Where is the mouse on the screen? (in pixels)
            Vector2 screenPosition = Mouse.current.position.ReadValue();

            // 2. Turn those pixels into a spot inside the game world.
            Vector2 worldPosition = cam.ScreenToWorldPoint(screenPosition);

            // 3. Fire a 2D ray at that spot and see what it hits.
            //    Direction Vector2.zero means "don't travel, just test this exact spot".
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            // 4. Hit nothing? The click missed — the player hit empty sky.
            if (hit.collider == null)
            {
                return;
            }

            // 5. We hit a collider. Is it a balloon?
            BalloonController balloon = hit.collider.GetComponent<BalloonController>();
            if (balloon != null)
            {
                balloon.Pop();
            }
        }
    }
}