using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SFXManager : Singleton<SFXManager>
{
    public AudioSource sfxAudioSource;

    [SerializeField] public AudioClip clickSound;
    [SerializeField] public AudioClip countdownSound;
    [SerializeField] public AudioClip explosionSound;

    void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();
        sfxAudioSource.enabled = true;
    }

    private void Update()
    {

    }
    public void PlaySound(AudioClip clip) 
    {
        sfxAudioSource.clip = clip;

        if (sfxAudioSource != null && !sfxAudioSource.isPlaying)
        {
            sfxAudioSource.Play();
        }
    }

    public void PlayOneShot(AudioClip clip) 
    {
        sfxAudioSource.PlayOneShot(clip);
    }
}
