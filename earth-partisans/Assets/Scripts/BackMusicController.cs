//This code has been witten by Behrang Abad Foomani﻿
using System.Collections;
using UnityEngine;

public class BackMusicController : MonoBehaviour
{
    private bool hasSelect;
    private uint audioClipIndex;
    public static AudioSource backgroundAudioSource;
    [SerializeField]
    private AudioClip[] audioClip;
    public static BackMusicController backMusicController;
    void Start()
    {
        if (backMusicController == null)
        {
            backMusicController = this;
            backgroundAudioSource = GetComponent<AudioSource>();
            Application.targetFrameRate = 100;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        if (hasSelect == false)
        {
            StartCoroutine(MusicChange());
        }
    }
    private IEnumerator MusicChange()
    {
        hasSelect = true;
        backgroundAudioSource.PlayOneShot(audioClip[Random.Range(0, 3)]);
        yield return new WaitForSeconds(audioClip[audioClipIndex].length);
        if (audioClipIndex == audioClip.Length - 1) { audioClipIndex = 0; }
        else { audioClipIndex++; }
        hasSelect = false;
    }
}
