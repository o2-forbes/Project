using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestBehaviour : MonoBehaviour
{
    public Animator animator;
    public AudioClip openSound; // Sound clip for opening the chest
    public GameObject afterTrigger; // Reference to the GameObject to activate after opening the chest
    public GameObject bartender; // Reference to the bartender object
    public Vector3 destinationPosition; // Destination position for the bartender to move to
    public Vector3 destinationRotation; // Destination rotation for the bartender to rotate to
    private AudioSource audioSource; // Reference to the AudioSource component
    private bool soundPlayed = false; // Flag to track whether the sound has been played

    // Start is called before the first frame update
    void Start()
    {
        // Get the reference to the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
        // Ensure the afterTrigger object is initially inactive
        if (afterTrigger != null)
        {
            afterTrigger.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !soundPlayed)
        {
            // Play animation
            animator.SetTrigger("Open");

            // Play sound effect if openSound is assigned and audioSource is not null
            if (openSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(openSound);
                soundPlayed = true; // Set the flag to true to indicate that the sound has been played
            }

            // Activate the afterTrigger object
            if (afterTrigger != null)
            {
                afterTrigger.SetActive(true);
            }

            // Move the bartender to the destination
            if (bartender != null)
            {
                bartender.transform.position = destinationPosition;
                bartender.transform.eulerAngles = destinationRotation;
            }
        }
    }
}
