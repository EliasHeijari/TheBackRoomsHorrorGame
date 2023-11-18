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
    private float timer = 0;
    [SerializeField] private const float timeToIncreaseHalluci = 2f;
    public UnityEvent hallucinationMoveFloor;
    public UnityEvent hallucinationStop;
    public static event EventHandler OnHallucinationWalls;
    public static event EventHandler OnHallucinationWallsOff;
    private bool experiencedHallucination = false;
    private bool hasWallsSet = false;

    enum hallucination
    {
        medium,
        crazy
    }

    private void Update()
    {
        IncreaseHallucination();
        UpdateUI();

        if (hallucinationLevel > maxHallucinationLevel / 4) // medium
        {
            HallucinationEffects(hallucination.medium);
        }
        else
        {
            OnHallucinationWallsOff?.Invoke(this, EventArgs.Empty);
        }
        if (hallucinationLevel > maxHallucinationLevel / 3) // crazy
        {
            HallucinationEffects(hallucination.crazy);
        }
    }

    private void UpdateUI()
    {
        hallucinationBar.fillAmount = Mathf.Lerp(hallucinationBar.fillAmount, ((float)hallucinationLevel / (float)maxHallucinationLevel), Time.deltaTime * 3);
    }

    private void HallucinationEffects(hallucination hallucination)
    {
        if (hallucination == hallucination.medium)
        {
            if (!hasWallsSet)
            {
                hasWallsSet = true;
                OnHallucinationWalls?.Invoke(this, EventArgs.Empty);
                StartCoroutine(StopWallsMovementAfterTime());
            }
        }
        if (hallucination == hallucination.crazy)
        {
            if (!experiencedHallucination)
            {
                OnHallucinationWalls?.Invoke(this, EventArgs.Empty);
                StartCoroutine(StopWallsMovementAfterTime());
                experiencedHallucination = true;
            }
            hallucinationBar.color = new Color(255, 0, 0);
        }
    }

    IEnumerator StopWallsMovementAfterTime()
    {
        yield return new WaitForSeconds(15f);
        OnHallucinationWallsOff?.Invoke(this, EventArgs.Empty);
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
