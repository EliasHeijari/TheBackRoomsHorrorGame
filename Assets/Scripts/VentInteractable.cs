using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject wrenchGameObject;
    string interactText = "Open Vent";
    public void Interact(Transform interactorTransform)
    {
        if (wrenchGameObject.activeSelf)
        {
            Destroy(gameObject);
            wrenchGameObject.SetActive(false);
        }
    }

    public string GetInteractText()
    {
        if (!wrenchGameObject.activeSelf)
        {
            interactText = "need a wrench to open";
        }
        else
        {
            interactText = "open vent";
        }
        return interactText;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
