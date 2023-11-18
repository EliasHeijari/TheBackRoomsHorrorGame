using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : MonoBehaviour
{
    // rotate to player, move forward until are close enough to player, then rotate towards player head and move towards it

    [SerializeField] private Transform playerTransform;
    [SerializeField] private Animator spiderAnimator;
    [SerializeField] private AudioSource jumpScareSource;
    [SerializeField] private AudioSource walkingSource;
    [SerializeField] private float moveSpeed = 10f;
    private float proximityThreshold = 2.5f;

    private void Start()
    {
        walkingSource.Play();
    }

    private void Update()
    {

        // Check the distance between this GameObject and the targetObject
        float distance = Vector3.Distance(transform.position, playerTransform.position);

        // If the distance is below the threshold, they are close enough
        if (distance < proximityThreshold)
        {
            spiderAnimator.SetBool("IsJump", true);
            if (!jumpScareSource.isPlaying) jumpScareSource.Play();

            // move towards head
            // Create a target position with the player's x and z coordinates
            Vector3 target = transform.position;
            target.x = playerTransform.position.x;
            target.y = playerTransform.position.y + 1f;
            target.z = playerTransform.position.z;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

            // Smoothly interpolate towards the target rotation
            transform.rotation = targetRotation;

            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
            Destroy(gameObject, 1.3f);
        }
        else
        {
            // Create a target position with the player's x and z coordinates
            Vector3 target = transform.position;
            target.x = playerTransform.position.x;
            target.z = playerTransform.position.z;

            // Calculate the target rotation
            Quaternion targetRotation = Quaternion.LookRotation(target - transform.position);

            // Smoothly interpolate towards the target rotation
            transform.rotation = targetRotation;

            // Move the GameObject forward locally
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        }
    }
}
