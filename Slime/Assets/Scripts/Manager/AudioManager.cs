using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("#BGM")]
    public AudioClip bgmClip;
    public float bgmVolume;
    AudioSource bgmPlayer;
    AudioHighPassFilter bgmEffect;

    [Header("#SFX")]
    public AudioClip[] sfxClips;
    public float sfxVolume;
    public int channels;
    int channelIndex;
    AudioSource[] sfxPlayers;

    public enum Sfx {Click1,  Click2, Click3, Clickback, Retry, Quite, Getgell, Getpet, Lvup, LvSelect, Attackpop, Spark, Gameover = 14, Gamestart};

    void Awake()
    {
        instance = this;
        Init();
    }

    void Init()
    {
        GameObject bgmobject = new GameObject("bgmPlayer");
        bgmobject.transform.parent = transform;
        bgmPlayer = bgmobject.AddComponent<AudioSource>();
        bgmPlayer.playOnAwake = false;
        bgmPlayer.loop = true;
        bgmPlayer.volume = bgmVolume;
        bgmPlayer.clip = bgmClip;
        bgmEffect = Camera.main.GetComponent<AudioHighPassFilter>();


        GameObject sfxObject = new GameObject("sfxPlayer");
        sfxObject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels];

        for (int i = 0; i < sfxPlayers.Length; i++)
        {
            sfxPlayers[i] = sfxObject.AddComponent<AudioSource>(); ;
            sfxPlayers[i].playOnAwake = false;
            sfxPlayers[i].volume = sfxVolume;
            sfxPlayers[i].bypassListenerEffects = true;
        }
    }

    public void PlayBgm(bool isplay)
    {
        if (isplay)
        {
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void EffectBgm(bool isplay)
    {
        bgmEffect.enabled = isplay;
    }

    public void PlaySfx(Sfx sfx)
    {
        for (int i=0; i < sfxPlayers.Length; i++)
        {
            int loopIndex =  ( i + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying) {
                continue;
            }

            int ranIndex = 0;
            if (sfx == Sfx.Spark)
            {
                ranIndex = Random.Range(0, 3);
            }

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx + ranIndex];
            sfxPlayers[loopIndex].Play();
            break;
        }

    }

}

