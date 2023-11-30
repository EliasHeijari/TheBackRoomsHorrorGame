using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowBattery : MonoBehaviour
{
    private float timer = 0;
    [SerializeField] private GameObject lowBatteryText;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1f)
        {
            lowBatteryText.SetActive(false);
        }
        else if (timer <= 1)
        {
            lowBatteryText.SetActive(true);
        }
        if (timer >= 2)
        {
            timer = 0;
        }
    }
}
