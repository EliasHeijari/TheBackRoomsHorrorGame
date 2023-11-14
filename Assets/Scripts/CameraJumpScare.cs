using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJumpScare : MonoBehaviour
{
    [SerializeField] private GameObject jumpScareObject;
    [SerializeField] private GameObject satan;
    [SerializeField] private GameObject enterWorldWall;
    private bool hasJumpScared = false;
    IEnumerator SetJumpScareOff()
    {
        yield return new WaitForSeconds(1.5f);
        jumpScareObject.SetActive(false);
    }
    IEnumerator SetJumpScareOn()
    {
        yield return new WaitForSeconds(2f);
        jumpScareObject.SetActive(true);
        StartCoroutine(SetJumpScareOff());
        satan.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player) && !hasJumpScared)
        {
            hasJumpScared = true;
            StartCoroutine(SetJumpScareOn());
            Destroy(enterWorldWall);
        }
    }
}
