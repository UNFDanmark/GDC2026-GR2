using UnityEngine;

public class AudioManager : MonoBehaviour
{
    AudioSource audioSource;

    AudioSource musicPlayer;

    bool playNextTick;

    AudioClip nextAudioClipToPlay;

    AudioClip backgroundMusic;

    RhythmManager rhythmManger;
    
    float delay;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicPlayer = GetComponentInChildren<AudioSource>();
        musicPlayer.clip = backgroundMusic;
        musicPlayer.Play();
        audioSource = GetComponent<AudioSource>();
        rhythmManger = GameObject.FindGameObjectWithTag("RhythmManager").GetComponent<RhythmManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playNextTick)
        {
            audioSource.clip = nextAudioClipToPlay;
            audioSource.PlayDelayed(rhythmManger.currentNoteSpeed); //LINE UPPPPPPPPKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKKK
            playNextTick = false;
        }
    }
    //rhythmManger.universalCardPlayDelay+
    public void PlayCardMelody(AudioClip melodyToPlay)
    {
        nextAudioClipToPlay = melodyToPlay;
        playNextTick = true;
    }
}
