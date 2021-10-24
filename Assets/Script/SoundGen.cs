using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundGen : MonoBehaviour
{

    [Header("Visual")]
    [SerializeField] private SoundCirlce soundCircleBase;
    [SerializeField] private Gradient feedBackColors;
    [SerializeField] private float maxColorThreshold;
    
    [Header("Audio")]
    [SerializeField] private AudioSource audio;
    [SerializeField] private List<AudioClip> walkingDefaultSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> runnningDefaultSounds = new List<AudioClip>();
    [SerializeField] private List<AudioClip> doorOpeningSounds = new List<AudioClip>();
    [SerializeField] private float maxSoundMultThreshold;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenSound(float soundMult)
    {
        if (!soundCircleBase)
            throw new NullReferenceException(
                "Fo rajouter une référence au prefab CercleSon dans le champ de référence \"Sound Circle Base\"");
        
        if (Instantiate(soundCircleBase.gameObject, transform.position, new Quaternion())
            .TryGetComponent(out SoundCirlce newSC))
        {
            float time = soundMult  / maxColorThreshold ;
            Color sCColor = feedBackColors.Evaluate(time);
            newSC.InitCircle(soundMult,sCColor);
        }
    }

    public void GenWalkingSound(float soundMult)
    {
        List<AudioClip> clips = walkingDefaultSounds;
        if (IsNoiseZone(out NoisyZone noisyZone))
        {
            soundMult += noisyZone.soundMult;
            clips = noisyZone.WalkSounds;
        }
        GenSound(soundMult);
        PlayAudio(clips, soundMult);
    }

    public void GenRunningSound(float soundMult)
    {
        List<AudioClip> clips = runnningDefaultSounds;
        if (IsNoiseZone(out NoisyZone noisyZone))
        {
            soundMult += noisyZone.soundMult;
            clips = noisyZone.RunSounds;
        }
        GenSound(soundMult);
        PlayAudio(clips, soundMult);
    }
    
    public void GenDoorOpeningSound(float soundMult)
    {
        List<AudioClip> clips = doorOpeningSounds;
        GenSound(soundMult);
        if(!audio.isPlaying)PlayAudio(clips, soundMult);
    }
    
    private bool IsNoiseZone(out NoisyZone noisyZone)
    {
        LayerMask noisyMask = LayerMask.GetMask("Noisy");
        Collider2D other = Physics2D.OverlapCircle(transform.position, 0.1f, noisyMask);
        
        if (other && other.TryGetComponent(out noisyZone)) {/*oups le if moyen util*/}
        else noisyZone = null;
        
        return other;
    }

    private void PlayAudio(AudioClip audioClip, float soundMult)
    {
        audio.volume = soundMult / maxSoundMultThreshold;
        audio.clip = audioClip;
        audio.Play();
    }

    private void PlayAudio([NotNull] List<AudioClip> clips, float soundMult)
    {
        if (clips == null) throw new ArgumentNullException(nameof(clips));
        PlayAudio(clips[Random.Range(0,clips.Count)], soundMult);
    }
    /*
    private IEnumerator TestGenSound()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            float alea = Random.Range(0.1f, 5);
            GenSound(alea);
            
        }
    }
    */
}
