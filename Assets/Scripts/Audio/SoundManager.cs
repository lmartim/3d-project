using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Core.Singleton;

public class SoundManager : Singleton<SoundManager>
{
    [Header("Audio Status")]
    public bool isAudioOn = true;

    [Header("UI")]
    public Image uiIndicator;

    [Header("Icons")]
    public Sprite audioOn;
    public Sprite audioOff;

    [Header("Audio Source")]
    public AudioSource musicSource;

    private AudioSource[] _audioSources;

    [Header("Setups")]
    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    private void Start()
    {
        if (isAudioOn) musicSource.enabled = true;
        else musicSource.enabled = false;

        Invoke("GetAudioSources", .3f);
    }

    private void GetAudioSources()
    {
        _audioSources = FindObjectsOfType<AudioSource>();
        SetAudio();
    }

    public void OnClick()
    {
        isAudioOn = !isAudioOn;
        SetAudio();
    }

    private void SetAudio()
    {
        if (isAudioOn)
        {
            uiIndicator.sprite = audioOn;
            SetAudioSources(true);
        }
        else
        {
            uiIndicator.sprite = audioOff;
            SetAudioSources(false);
        }
    }

    private void SetAudioSources(bool status)
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.enabled = status;
        }
    }


    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }
}

public enum MusicType
{
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

public enum SFXType
{
    NONE,
    TYPE_01,
    TYPE_02,
    TYPE_03
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip sfxClip;
}
