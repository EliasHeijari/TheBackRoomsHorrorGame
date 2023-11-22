using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBattery : MonoBehaviour
{
    // 0.2 = one battery bar
    // battery has 5 bars
    private int batteryLife;
    private int maxBatteryLife = 5;
    private float barSize = 0.2f;
    [SerializeField] private Image batteryBarsImage;
    [SerializeField] private float timeToDecreaseBattery = 60f;
    private void Start()
    {
        batteryLife = maxBatteryLife;
        InvokeRepeating("DecreaseBatteryLife", timeToDecreaseBattery, timeToDecreaseBattery);
        BatteryInteractable.OnBatteryTaken += BatteryInteractable_OnBatteryTaken;
    }

    private void BatteryInteractable_OnBatteryTaken(object sender, BatteryInteractable.BatteryTakenEventArgs e)
    {
        if (batteryLife < maxBatteryLife)
        {
            batteryLife++;
            batteryBarsImage.fillAmount += barSize;
            if (batteryLife > 2) batteryBarsImage.color = new Color(1, 1, 1, 0.35f);
            Destroy(e.batteryGameObject);
        }
    }

    public void DecreaseBatteryLife()
    {
        if (batteryLife > 1)
        {
            batteryLife--;
            batteryBarsImage.fillAmount -= barSize;
            if (batteryLife <= 2) batteryBarsImage.color = new Color(1, 0, 0, 0.35f);
        }
        else
        {
            batteryLife = 0;
            batteryBarsImage.fillAmount = 0;
            Debug.Log("Signal Lost, Battery Died");
        }
    }
}
