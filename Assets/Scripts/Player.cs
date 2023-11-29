using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private string partygoerTag;
    [SerializeField] private string smilingTag;
    [SerializeField] private string bacteriaTag;
    [SerializeField] private Transform partyGoerTransform;
    [SerializeField] private Transform bacteriaTransform;
    [SerializeField] private Transform smilingTransform;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private AudioSource JumpScareSource;
    [SerializeField] private int EndSceneBuildIndex;
    bool isDead = false;

    private CharacterController characterController;
    private PlayerController playerController;

    public static event EventHandler<JumpScareEventArgs> OnJumpScare;

    public class JumpScareEventArgs : EventArgs
    {
        public string tag;
    }

    private void Start()
    {
        health = maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
        playerController = GetComponent<PlayerController>();
        StartCoroutine(EnableControlsAfterTime());
    }

    public void TakeDamage(int damage, string tag)
    {
        health -= damage;
        if (health <= 0)
        {
            if (!isDead)
            {
                Die(tag);
                isDead = true;
            }
        }
    }

    private void Die(string killersTag)
    {
        DisableInputs();
        JumpScareSource.Play();
        StartCoroutine(LoadEndScene());
        if (killersTag == partygoerTag)
        {
            //partygoerJumpScare
            Debug.Log("PartyGoerJumpScare");
            StartCoroutine(LookAtAfterTime(partyGoerTransform));
            OnJumpScare?.Invoke(this, new JumpScareEventArgs { tag = partygoerTag});
        }
        else if (killersTag == smilingTag)
        {
            //smilingManJumpScare
            Debug.Log("SmilingManJumpScare");
            StartCoroutine(LookAtAfterTime(smilingTransform));
            OnJumpScare?.Invoke(this, new JumpScareEventArgs { tag = smilingTag });
        }
        else if (killersTag == bacteriaTag)
        {
            //smilingManJumpScare
            Debug.Log("BacteriaJumpScare");
            StartCoroutine(LookAtAfterTime(bacteriaTransform));
            OnJumpScare?.Invoke(this, new JumpScareEventArgs { tag = bacteriaTag });
        }
        else
        {
            Debug.LogWarning("No Killers Tag Found! Check Tags");
        }
    }

    IEnumerator LoadEndScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadSceneAsync(EndSceneBuildIndex);
    }

    IEnumerator LookAtAfterTime(Transform targetTransform)
    {
        yield return new WaitForSeconds(0.3f);
        cameraObject.transform.LookAt(targetTransform.position + 2.5f * Vector3.up);
    }

    private void DisableInputs()
    {
        GetComponent<PlayerController>().enabled = false;
        cameraObject.GetComponent<HeadBob>().enabled = false;
    }

    IEnumerator EnableControlsAfterTime()
    {
        yield return new WaitForSeconds(3f);
        characterController.enabled = true;
        playerController.enabled = true;
    }

    public int GetHealth() { return health; }
}
