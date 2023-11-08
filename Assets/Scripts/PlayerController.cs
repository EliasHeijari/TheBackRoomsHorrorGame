//Script by EVOLVE GAMES > License = BY
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
    [SerializeField] float RuningSpeed = 7.0f;
    [SerializeField] float jumpSpeed = 6.0f;
    [SerializeField] float lookSpeed = 2.0f;
    [SerializeField] float lookXLimit = 60.0f;
    [Space(20)]
    [Header("Advance")]
    [SerializeField] float RunningFOV = 65.0f;
    [SerializeField] float SpeedToFOV = 4.0f;
    [SerializeField] float CroughHeight = 1.0f;
    [SerializeField] float gravity = 20.0f;
    [SerializeField] float timeToRunning = 10.0f;
    private bool canMove = true;
    [Space(20)]
    [Header("Input")]
    [SerializeField] KeyCode CroughKey = KeyCode.LeftControl;
    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    bool isCrough;
    float InstallCroughHeight;
    float rotationX = 0;
    private bool isRunning = false;
    
    float InstallWalkingSpeed;
    float InstallFOV;
    Camera cam;
    private bool Moving;
    private float vertical;
    private float horizontal;
    private float Lookvertical;
    private float Lookhorizontal;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        InstallCroughHeight = characterController.height;
        InstallFOV = cam.fieldOfView;
        InstallWalkingSpeed = walkingSpeed;
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
        if (isRunning)
        {
            vertical = Mathf.Lerp(walkingSpeed, RuningSpeed, timeToRunning * Time.deltaTime) * Input.GetAxis("Vertical");
            horizontal = Mathf.Lerp(walkingSpeed, RuningSpeed, timeToRunning * Time.deltaTime) * Input.GetAxis("Horizontal");
        }
        else
        {
            vertical = walkingSpeed * Input.GetAxis("Vertical");
            horizontal = walkingSpeed * Input.GetAxis("Horizontal");
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
        Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

        if (Cursor.lockState == CursorLockMode.Locked && canMove)
        {
            Lookvertical = -Input.GetAxis("Mouse Y");
            Lookhorizontal = Input.GetAxis("Mouse X");

            rotationX += Lookvertical * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Lookhorizontal * lookSpeed, 0);

            if (isRunning && Moving)
            {
                cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, RunningFOV, SpeedToFOV * Time.deltaTime);
            }
            else cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, SpeedToFOV * Time.deltaTime);
        }


        if (Input.GetKey(CroughKey))
        {
            float Height = Mathf.Lerp(characterController.height, CroughHeight, 5 * Time.deltaTime);
            characterController.height = Height;
            walkingSpeed = Mathf.Lerp(walkingSpeed, CroughSpeed, 6 * Time.deltaTime);
        }
        else
        {
            float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 7 * Time.deltaTime);
            characterController.height = Height;
            walkingSpeed = Mathf.Lerp(walkingSpeed, InstallWalkingSpeed, 4 * Time.deltaTime);
        }
    }
}
