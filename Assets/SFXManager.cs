using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [Serializable]
    public struct AudioInfo
    {
        string audioName;
        AudioClip audioClip;
    }

    AudioSource[] audiopool;
    public int poolSize = 10;
    public AudioDictionary audioInfos;
    [Range(0f,1f)]
    public float defaultVolume = 0.5f;
    void Start()
    {
        audiopool = new AudioSource[poolSize];
        for(int i=0; i < audiopool.Length; i++)
        {
            audiopool[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    void ExpandPool()
    {
        int firstIndex = audiopool.Length;
        Array.Resize(ref audiopool, audiopool.Length + poolSize);

        for (int i = firstIndex; i < audiopool.Length; i++)
        {
            audiopool[i] = gameObject.AddComponent<AudioSource>();
        }
    }
    public void PlayAudio(string audioName)
    {
        PlayAudio(audioName, defaultVolume);
    }
    public int PlayAudio(string audioName,float volume)
    {
        bool full = true;
        int currentIndex = -1;
        foreach(AudioSource audioSource in audiopool)
        {
            currentIndex += 1;
            if(audioSource.isPlaying == false)
            {
                full = false;
                break;
            }
        }
        if(full)
        {
            ExpandPool();
            currentIndex = audiopool.Length - 1;
        }
        audiopool[currentIndex].loop = false;
        audiopool[currentIndex].volume = volume;
        audiopool[currentIndex].clip = audioInfos[audioName];
        audiopool[currentIndex].Play();
        return currentIndex;
    }

    public void PlayAudioLooping(string audioName)
    {
       PlayAudioLooping(audioName, defaultVolume);
    }

    public int PlayAudioLooping(string audioName, float volume)
    {
        int i = PlayAudio(audioName, volume);
        audiopool[i].loop = true;
        return i;
    }

    public void StopAudioByIndex(int index)
    {
        if(index < audiopool.Length && index > -1)
            audiopool[index].Stop();
    }

    public void StopAudioByName(string audioName)
    {
        foreach (AudioSource audioSource in audiopool)
        {
            if (audioSource.clip == audioInfos[audioName])
            {
                audioSource.Stop();
            }
        }
    }

    public void StopAll()
    {
        foreach (AudioSource audioSource in audiopool)
        {
            audioSource.Stop();
        }
    }
}
