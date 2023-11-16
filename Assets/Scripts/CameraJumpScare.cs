using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraJumpScare : MonoBehaviour
{
    [SerializeField] private GameObject jumpScareObject;
    [SerializeField] private GameObject satan;
    [SerializeField] private GameObject enterWorldWall;
    private bool hasJumpScared = false;
    [SerializeField] private AudioSource lightsOffSource;

    private void Start()
    {
        enterWorldWall.SetActive(true);
    }
    IEnumerator SetJumpScareOff()
    {
        yield return new WaitForSeconds(2f);
        jumpScareObject.SetActive(false);
        RenderSettings.fog = true;
        Color color = new Color(0.35f, 0.35f, 0.35f);
        RenderSettings.ambientLight = color;
        satan.gameObject.SetActive(true);
        lightsOffSource.Play();
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
            Destroy(enterWorldWall);
        }
    }
}
