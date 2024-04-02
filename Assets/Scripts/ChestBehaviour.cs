using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public Animator animator;
    public AudioClip openSound; // Sound clip for opening the chest
    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Play animation
            animator.SetTrigger("Open");

            // Play sound effect if openSound is assigned
            if (openSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(openSound);
            }
        }
    }
}
