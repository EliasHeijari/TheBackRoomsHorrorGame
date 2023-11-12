using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Hallucination : MonoBehaviour
{
    [SerializeField] private const int maxHallucinationLevel = 100;
    [SerializeField] private int hallucinationLevel = 5;
    [SerializeField] private Image hallucinationBar;
    [SerializeField] private GameObject flashCharacter;
    [SerializeField] private Transform jumpScareTransform;
    [SerializeField] private GameObject satan;
    private GameObject jumpScareCharacter;
    private float timer = 0;
    [SerializeField] private const float timeToIncreaseHalluci = 2f;
    private float mediumJumpScareTimer = 0;
    private float timeToMediumJumpScare = 0;
    private bool mediumJumpScareTimeSet = false;
    public UnityEvent hallucinationMoveFloor;
    public UnityEvent hallucinationStop;
    public static event EventHandler OnHallucinationMedium;
    public static event EventHandler OnHallucinationMediumOff;
    private bool experiencedHallucination = false;
    private bool hasWallsSet = false;

    enum hallucination
    {
        small,
        medium,
        crazy
    }

    private void Update()
    {
        IncreaseHallucination();
        UpdateUI();

        if (hallucinationLevel > maxHallucinationLevel / 5) // small
        {
            HallucinationEffects(hallucination.small);
        }
        if (hallucinationLevel > maxHallucinationLevel / 4) // medium
        {
            HallucinationEffects(hallucination.medium);
        }
        else
        {
            OnHallucinationMediumOff?.Invoke(this, EventArgs.Empty);
        }
        if (hallucinationLevel > maxHallucinationLevel / 2) // crazy
        {
            HallucinationEffects(hallucination.crazy);
        }
        else { satan.gameObject.SetActive(false); }
        if (hallucinationLevel >= maxHallucinationLevel) // die
        {
            Debug.Log("you died");
        }
    }

    private void UpdateUI()
    {
        hallucinationBar.fillAmount = Mathf.Lerp(hallucinationBar.fillAmount, ((float)hallucinationLevel / (float)maxHallucinationLevel), Time.deltaTime * 3);
    }

    private void HallucinationEffects(hallucination hallucination)
    {
        if (hallucination == hallucination.small)
        {
            SpawnJumpScare();
        }
        if (hallucination == hallucination.medium)
        {
            if (!hasWallsSet)
            {
                hasWallsSet = true;
                OnHallucinationMedium?.Invoke(this, EventArgs.Empty);
                StartCoroutine(StopWallsMovementAfterTime());
            }

            satan.gameObject.SetActive(true);
        }
        if (hallucination == hallucination.crazy)
        {
            if (!experiencedHallucination)
            {
                hallucinationMoveFloor.Invoke();
                StartCoroutine(StopFloorMovementAfterTime());
                experiencedHallucination = true;
            }
            hallucinationBar.color = new Color(255, 0, 0);
        }
    }

    IEnumerator StopFloorMovementAfterTime()
    {
        yield return new WaitForSeconds(6f);
        hallucinationStop.Invoke();
    }//OnHallucinationMediumOff?.Invoke(this, EventArgs.Empty);

    IEnumerator StopWallsMovementAfterTime()
    {
        yield return new WaitForSeconds(15f);
        OnHallucinationMediumOff?.Invoke(this, EventArgs.Empty);
    }

    private void SpawnJumpScare()
    {
        mediumJumpScareTimer += Time.deltaTime;
        if (!mediumJumpScareTimeSet)
        {
            timeToMediumJumpScare = UnityEngine.Random.Range(20f, 40f);
            mediumJumpScareTimeSet = true;
        }
        if (mediumJumpScareTimer >= timeToMediumJumpScare)
        {
            jumpScareCharacter = Instantiate(flashCharacter, jumpScareTransform.position, Quaternion.identity);
            jumpScareCharacter.transform.LookAt(transform.position - Vector3.up);
            Destroy(jumpScareCharacter, 2.3f);
            mediumJumpScareTimeSet = false;
            mediumJumpScareTimer = 0;
        }
    }

    private void IncreaseHallucination()
    {
        timer += Time.deltaTime;
        if (timer >= timeToIncreaseHalluci)
        {
            timer = 0;
            hallucinationLevel++;
        }
    }

    public void DecreaseHallucination(int amount)
    {
        hallucinationLevel -= amount;
        if (hallucinationLevel <= 0) hallucinationLevel = 0;
    }
}
