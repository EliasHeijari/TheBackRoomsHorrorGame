using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJumpScare : MonoBehaviour
{
    [SerializeField] private GameObject jumpScareObject;
    [SerializeField] private float timeToLightsOff = 15f;
    private bool hasJumpScared = false;
    [SerializeField] private AudioSource startSoundSource;

    private void Start()
    {
        StartCoroutine(PlaySound());
    }

    IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(timeToLightsOff);
        startSoundSource.Play();
    }
    IEnumerator SetJumpScareOff()
    {
        yield return new WaitForSeconds(2f);
        jumpScareObject.SetActive(false);
    }

    IEnumerator SetJumpScareOn()
    {
        yield return new WaitForSeconds(3f);
        jumpScareObject.SetActive(true);
        StartCoroutine(SetJumpScareOff());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player) && !hasJumpScared)
        {
            hasJumpScared = true;
            StartCoroutine(SetJumpScareOn());
        }
    }
}
