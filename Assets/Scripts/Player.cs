using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private string partygoerTag;
    [SerializeField] private string smilingTag;
    [SerializeField] private Transform partyGoerTransform;
    [SerializeField] private Transform smilingTransform;
    [SerializeField] private GameObject cameraObject;

    public static event EventHandler<JumpScareEventArgs> OnJumpScare;

    public class JumpScareEventArgs : EventArgs
    {
        public string tag;
    }

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(int damage, string tag)
    {
        health -= damage;
        if (health <= 0)
        {
            Die(tag);
        }
    }

    private void Die(string killersTag)
    {
        DisableInputs();
        if (killersTag == partygoerTag)
        {
            //partygoerJumpScare
            Debug.Log("PartyGoerJumpScare");
            cameraObject.transform.LookAt(partyGoerTransform.position + 2.5f * Vector3.up);
            OnJumpScare?.Invoke(this, new JumpScareEventArgs { tag = partygoerTag});
        }
        else if (killersTag == smilingTag)
        {
            //smilingManJumpScare
            Debug.Log("SmilingManJumpScare");
            cameraObject.transform.LookAt(smilingTransform.position + 2.5f * Vector3.up);
            OnJumpScare?.Invoke(this, new JumpScareEventArgs { tag = smilingTag });
        }
        else
        {
            Debug.LogWarning("No Killers Tag Found! Check Tags");
        }
    }

    private void DisableInputs()
    {
        GetComponent<PlayerController>().enabled = false;
        cameraObject.GetComponent<HeadBob>().enabled = false;
    }

    public int GetHealth() { return health; }
}
