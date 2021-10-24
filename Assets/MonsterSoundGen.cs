using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSoundGen : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private List<AudioClip> walkingDefaultSounds = new List<AudioClip>();
    [SerializeField] public AudioClip roarSound;
    [SerializeField] public AudioSource RoarSource;
    // Start is called before the first frame update
    public void GenWalkingSound(float soundMult)
    {
        List<AudioClip> clips = walkingDefaultSounds;
        PlayAudio(clips[0], soundMult);
    }
    public void GenRoarSound(float soundMult)
    {
        
        if (!audio.isPlaying) RoarSource.PlayOneShot(roarSound);

    }
    private void PlayAudio(AudioClip audioClip, float soundMult)
    {
        audio.volume = soundMult;
        audio.clip = audioClip;
        audio.pitch = Random.Range(-3.0f, 1.0f);
        audio.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
