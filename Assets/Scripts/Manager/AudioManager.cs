using System.Collections;
using System.Collections.Generic;
using ByteLoop.Manager;
using ByteLoop.Tool;
using System;
using UnityEngine;

public class AudioManager :PersistentMonoSingleton<AudioManager>
{
    [SerializeField] public AudioClip[] audioClips;
    // public Dictionary<string,AudioClip> audioDic = new Dictionary<string, AudioClip>();
    [SerializeField] public AudioSource audioSource;

    private AudioListener audioListener;    

    public void Play(Music music){
        if(audioSource==null) return;
        // AudioClip audioClip = Resources.Load<AudioClip>(name);
        // audioSource.clip = audioClip;
        // audioSource.Play();
        audioSource.PlayOneShot(audioClips[(int)music]);
    }

// 
    public void PlayBGM(Music music,bool loop){
        // Debug.Log(bgmName);
        if(audioSource==null) return;
        AudioClip audioClip = audioClips[(int)music];
        Debug.Log(audioClip);
        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    private void CheckAudioListener(){
        if(audioListener==null){
            this.gameObject.AddComponent<AudioListener>();
        }
    }
    public void Stop(){
        if(audioSource==null) return;
        audioSource.Stop();
    }


   



}

 public  enum Music{
        None=-1,
        MainMenuBGM = 0,
        MenuBGM =1,
        Fifth=2,
        First=3,
        Fourth=4,
        Second=5,
        Third=6,
    }