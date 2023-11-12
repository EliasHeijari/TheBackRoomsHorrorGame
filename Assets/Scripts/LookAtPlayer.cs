using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float rotationSpeed = 1.5f;

    private void Update()
    {
        // Create a target position with the player's x and z coordinates
        Vector3 target = transform.position;
        target.x = playerTransform.position.x;
        target.z = playerTransform.position.z;

        // Calculate the target rotation
        Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

        // Smoothly interpolate towards the target rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}

