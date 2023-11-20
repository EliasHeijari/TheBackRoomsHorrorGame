using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillBottleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Hallucination hallucination;
    [SerializeField] private int medHelpAmount = 5;
    public void Interact(Transform interactorTransform)
    {
        Destroy(gameObject);
    }

    public string GetInteractText()
    {
        return "Eat Pills";
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
