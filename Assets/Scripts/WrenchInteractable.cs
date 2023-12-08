using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject wrenchGameObject;
    public void Interact(Transform interactorTransform)
    {
        Destroy(gameObject);
        wrenchGameObject.SetActive(true);
    }

    public string GetInteractText()
    {
        return "Pick Up Wrench";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
