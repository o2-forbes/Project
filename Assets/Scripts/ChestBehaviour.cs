using UnityEngine;

public class StopMusicOnTrigger : MonoBehaviour
{
    public GameObject musicPlayer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopMusic();
        }
    }

    private void StopMusic()
    {
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
