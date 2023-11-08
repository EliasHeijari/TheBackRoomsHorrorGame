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

    private void Update()
    {
        if (stopChaseAudio)
        {
            ChaseAudioSource.volume = Mathf.Lerp(ChaseAudioSource.volume, 0, fadeToStopChaseAudio * Time.deltaTime);
            if (ChaseAudioSource.volume < 0.1)
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
            ChaseAudioSource.volume = 1f;
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
