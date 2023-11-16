using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource ComeHereAudioSource;
    [SerializeField] private AudioSource ChaseAudioSource;
    [SerializeField] private float fadeToStopChaseAudio = 3;
    private float canPlayTime = 20f;
    bool canPlayOverHere = true;
    bool stopChaseAudio = false;
    private float chaseAudioVolume;

    private void Start()
    {
        chaseAudioVolume = ChaseAudioSource.volume;
    }

    private void Update()
    {
        if (stopChaseAudio && ChaseAudioSource.volume > 0.03f)
        {
            ChaseAudioSource.volume = Mathf.Lerp(ChaseAudioSource.volume, 0, fadeToStopChaseAudio * Time.deltaTime);
            if (ChaseAudioSource.volume < 0.03f)
            {
                ChaseAudioSource.Stop();
            }
        }
    }
    public void PlayChaseAudios()
    {
        if (!ComeHereAudioSource.isPlaying)
        {
            if (canPlayOverHere)
            {
                canPlayOverHere = false;
                ComeHereAudioSource.Play();
                StartCoroutine(StartCountingCanPlay());
            }
        }
        if (!ChaseAudioSource.isPlaying) 
        {
            stopChaseAudio = false;
            ChaseAudioSource.volume = chaseAudioVolume;
            ChaseAudioSource.Play();
        }
    }

    public void StopChaseAudios()
    {
        ComeHereAudioSource.Stop();
        stopChaseAudio = true;
    }

    IEnumerator StartCountingCanPlay()
    {
        yield return new WaitForSeconds(canPlayTime);
        canPlayOverHere = true;
    }
}
