using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour
{
    [Header("Main")]
    [SerializeField] Transform Camera;
    [SerializeField] float walkingSpeed = 3.0f;
    [SerializeField] float CroughSpeed = 1.0f;
    [SerializeField] float RuningSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 6.0f;
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 60.0f;
    [Space(20)]
    [Header("Advance")]
    [SerializeField] float RunningFOV = 65.0f;
    [SerializeField] float SpeedToFOV = 4.0f;
    [SerializeField] float CroughHeight = 0.5f;
    [SerializeField] float gravity = 20.0f;
    private bool canMove = true;
    [Space(20)]
    [Header("Input")]
    [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    bool isCrough;
    bool isCroughing;
    float InstallCroughHeight;
    float rotationX = 0;
    private bool isRunning = false;
    private float InstallRuningSpeed;
    float InstallWalkingSpeed;
    float InstallFOV;
    Cinemachine.CinemachineVirtualCamera cam;
    private bool Moving;
    private float vertical;
    private float horizontal;
    private float Lookvertical;
    private float Lookhorizontal;

    [Header("walking & running Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkingClip;
    [SerializeField] private AudioClip runningClip;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Cinemachine.CinemachineVirtualCamera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InstallCroughHeight = characterController.height;
        InstallFOV = cam.m_Lens.FieldOfView;
        InstallWalkingSpeed = walkingSpeed;
        InstallRuningSpeed = RuningSpeed;
    }

    void Update()
    {
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        if (!isCrough) isRunning = Input.GetKey(KeyCode.LeftShift);

        float targetSpeed = isRunning ? RuningSpeed : walkingSpeed;

        // Get raw input values without multiplying by speed
        float inputVertical = Input.GetAxis("Vertical");
        float inputHorizontal = Input.GetAxis("Horizontal");

        // Normalize the input vector to prevent faster movement diagonally
        Vector3 inputVector = new Vector3(inputHorizontal, 0, inputVertical).normalized;
        vertical = targetSpeed * inputVector.z;
        horizontal = targetSpeed * inputVector.x;

        if (isRunning && (Mathf.Abs(inputHorizontal) > 0 || Mathf.Abs(inputVertical) > 0))
        {
            audioSource.clip = runningClip;
            if (!audioSource.isPlaying) audioSource.Play();
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 3;
        }
        else if (isRunning)
        {
            audioSource.Stop();
        }

        if (!isRunning && (Mathf.Abs(inputHorizontal) > 0 || Mathf.Abs(inputVertical) > 0))
        {
            audioSource.clip = walkingClip;
            if (!audioSource.isPlaying) audioSource.Play();
        }
        else if (!isRunning)
        {
            audioSource.Stop();
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1;
        }

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * vertical) + (right * horizontal);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        characterController.Move(moveDirection * Time.deltaTime);
        Moving = Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0;

        if (Cursor.lockState == CursorLockMode.Locked && canMove)
        {
            Lookvertical = -Input.GetAxis("Mouse Y");
            Lookhorizontal = Input.GetAxis("Mouse X");

            rotationX += Lookvertical * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

            float targetFOV = isRunning && Moving ? RunningFOV : InstallFOV;
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, targetFOV, SpeedToFOV * Time.deltaTime);
        }

        float targetHeight = isCroughing ? CroughHeight : InstallCroughHeight;
        float targetWalkingSpeed;
        if (isCroughing)
        {
            targetWalkingSpeed = CroughSpeed;
            RuningSpeed = CroughSpeed * 1.6f;
        }
        else
        {
            RuningSpeed = InstallRuningSpeed;
            targetWalkingSpeed = InstallWalkingSpeed;
        }

        characterController.height = Mathf.Lerp(characterController.height, targetHeight, 5 * Time.deltaTime);
        walkingSpeed = Mathf.Lerp(walkingSpeed, targetWalkingSpeed, 6 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Vent"))
        {
            isCroughing = true;
        }
    }
}


