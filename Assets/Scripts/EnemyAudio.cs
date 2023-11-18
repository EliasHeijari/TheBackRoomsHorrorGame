using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource NoticesAudioSource;
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
        if (!NoticesAudioSource.isPlaying)
        {
            if (canPlayOverHere)
            {
                canPlayOverHere = false;
                NoticesAudioSource.Play();
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
        NoticesAudioSource.Stop();
        stopChaseAudio = true;
    }

    IEnumerator StartCountingCanPlay()
    {
        yield return new WaitForSeconds(canPlayTime);
        canPlayOverHere = true;
    }
}
