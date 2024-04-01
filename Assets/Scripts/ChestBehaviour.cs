using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public Animator chestAnimator;
    public AudioSource chestSoundEffect;
    bool hasBeenOpened = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenOpened)
        {
            chestAnimator.SetTrigger("Open");

            chestSoundEffect.Play();

            hasBeenOpened = true;
        }
    }
}
