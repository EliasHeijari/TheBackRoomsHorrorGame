using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryInteractable : MonoBehaviour, IInteractable
{
    private string text = "Take Battery";
    public class BatteryTakenEventArgs : EventArgs
    {
        public GameObject batteryGameObject;
    }

    public static event EventHandler<BatteryTakenEventArgs> OnBatteryTaken;

    public void Interact(Transform interactorTransform)
    {
        OnBatteryTaken?.Invoke(this, new BatteryTakenEventArgs { batteryGameObject = gameObject });
        text = "\nBattery already full";
    }

    public string GetInteractText()
    {
        return text;
    }

    public Transform GetTransform()
    {
        return transform;
    }

}
