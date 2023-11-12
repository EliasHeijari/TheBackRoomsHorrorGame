using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoclipTVInteractable : MonoBehaviour, IInteractable
{
    public void Interact(Transform interactorTransform)
    {
        Destroy(gameObject);
    }

    public string GetInteractText()
    {
        return "\n Noclip \n Out Of Here";
    }

    public Transform GetTransform()
    {
        return transform;
    }

}
