using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJumpScare : MonoBehaviour
{
    [SerializeField] private GameObject jumpScareObject;
    private bool hasJumpScared = false;
    IEnumerator SetJumpScareOff()
    {
        yield return new WaitForSeconds(2.3f);
        jumpScareObject.SetActive(false);
    }
    IEnumerator SetJumpScareOn()
    {
        yield return new WaitForSeconds(2f);
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
