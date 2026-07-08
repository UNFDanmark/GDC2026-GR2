using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    bool playNextTick;

    AudioClip nextAudioClipToPlay;

    float delay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playNextTick)
        {
            audioSource.clip = nextAudioClipToPlay;
            audioSource.PlayDelayed(delay);
            playNextTick = false;
        }
    }

    public void PlayCardMelody(AudioClip melodyToPlay)
    {
        nextAudioClipToPlay = melodyToPlay;
        playNextTick = true;
    }
}
