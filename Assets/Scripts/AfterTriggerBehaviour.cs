using UnityEngine;

public class AfterTriggerBehaviour : MonoBehaviour
{
    public AudioSource soundEffect; 
    public GameObject musicPlayer; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            if (soundEffect != null)
            {
                soundEffect.Play();
            }

            if (musicPlayer != null)
            {
                AudioSource musicAudioSource = musicPlayer.GetComponent<AudioSource>();
                if (musicAudioSource != null && musicAudioSource.isPlaying)
                {
                    musicAudioSource.Stop();
                }
            }
        }
    }
}
