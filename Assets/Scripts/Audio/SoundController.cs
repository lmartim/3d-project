using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Header("Audio Status")]
    public bool isAudioOn = true;

    [Header("UI")]
    public Image uiIndicator;

    [Header("Icons")]
    public Sprite audioOn;
    public Sprite audioOff;

    [Header("Audio Source")]
    public AudioSource audioSource;

    private AudioSource[] _audioSources;

    private void Start()
    {
        if (isAudioOn) audioSource.enabled = true;
        else audioSource.enabled = false;

        Invoke("GetAudioSources", .3f);
    }

    private void GetAudioSources()
    {
        _audioSources = FindObjectsOfType<AudioSource>();
        SetAudio();
    }

    private void OnMouseDown()
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
}
