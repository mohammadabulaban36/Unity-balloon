using UnityEngine;

// Plays the game's sound effects.
public class SoundControoller : MonoBehaviour
{

    [SerializeField] AudioSource sourceEffect;      // the effects speaker
    [SerializeField] AudioClip destroyBalloonClip;  // the pop sound

    public void PlayDestroyBalloonEffect()
    {
        sourceEffect.PlayOneShot(destroyBalloonClip);
    }
}