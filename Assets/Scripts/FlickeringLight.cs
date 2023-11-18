using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    [SerializeField] private float minIntensity = 0.5f;
    [SerializeField] private float maxIntensity = 1.5f;
    [SerializeField] private float flickerSpeed = 0.2f;

    private Light flickeringLight;

    void Start()
    {
        flickeringLight = GetComponent<Light>();
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            // Randomly adjust the intensity within the specified range
            flickeringLight.intensity = Random.Range(minIntensity, maxIntensity);

            // Wait for a short duration to create the flickering effect
            yield return new WaitForSeconds(flickerSpeed);
        }
    }
}
