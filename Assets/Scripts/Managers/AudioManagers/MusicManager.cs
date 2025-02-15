using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    public AudioSource musicSource;
    private int currentMusicIndex;
    [SerializeField] private AudioClip[] musicClip;
    private float delay = 1.0f;

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        currentMusicIndex = 0;
        musicSource.clip = musicClip[0];
        musicSource.Play();
        currentMusicIndex++;
    }

    // Update is called once per frame
    void Update()
    {
        if (!musicSource.isPlaying) 
        {
            PlayMusic();
        }
    }

    public void PlayMusic() 
    {
        musicSource.clip = musicClip[currentMusicIndex];
        currentMusicIndex = (currentMusicIndex + 1) % musicClip.Length;
        musicSource.PlayDelayed(delay);
    }

}
