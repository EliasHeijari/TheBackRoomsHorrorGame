using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string slide;
    public void Interact(Transform interactorTransform)
    {

    }

    public string GetInteractText()
    {
        return "Go To The Slide";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
