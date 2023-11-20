using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Hallucination : MonoBehaviour
{
    [SerializeField] private const int maxHallucinationLevel = 100;
    [SerializeField] private int hallucinationLevel = 5;
    [SerializeField] private const float timeToIncreaseHalluci = 2f;
    public static event EventHandler OnHallucinationWalls;
    public static event EventHandler OnHallucinationWallsOff;
    private bool experiencedCrazyHallucination = false;
    private bool experiencedMediumHallucination = false;
    private bool experiencedSmallHallucination = false;
    enum hallucination
    {
        small,
        medium,
        crazy
    }

    private void Update()
    {
        if (PlayerInteract.pillsEaten >= 5 && !experiencedCrazyHallucination)
        {
            HallucinationEffects(hallucination.crazy);
            experiencedCrazyHallucination = true;
        }
        else if (PlayerInteract.pillsEaten >= 3 && !experiencedMediumHallucination)
        {
            HallucinationEffects(hallucination.medium);
            experiencedMediumHallucination = true;
        }
        else if (PlayerInteract.pillsEaten >= 1 && !experiencedSmallHallucination)
        {
            HallucinationEffects(hallucination.small);
            experiencedSmallHallucination = true;
        }
    }

    private void HallucinationEffects(hallucination hallucination)
    {

        // small hallucination, when player has eaten 1 pills
        if (hallucination == hallucination.small)
        {

        }

        // medium hallucination, when player has eaten 3 pills
        if (hallucination == hallucination.medium)
        {
            // Walls Start Moving And Stops After Some Time
            OnHallucinationWalls?.Invoke(this, EventArgs.Empty);
            StartCoroutine(StopWallsMovementAfterTime());
        }

        // crazy hallucination, when player has eaten 5 pills
        if (hallucination == hallucination.crazy)
        {
            // Walls Start Moving And Stops After Some Time
            OnHallucinationWalls?.Invoke(this, EventArgs.Empty);
            StartCoroutine(StopWallsMovementAfterTime());

            // More Halluzination Effects, Audio etc
        }
    }

    IEnumerator StopWallsMovementAfterTime()
    {
        yield return new WaitForSeconds(15f);
        OnHallucinationWallsOff?.Invoke(this, EventArgs.Empty);
    }
}
