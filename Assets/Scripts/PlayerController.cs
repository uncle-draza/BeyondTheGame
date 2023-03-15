using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f; //ovo da promenim (povecam) da bih mogao da vidim pravo iznad sebe
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    public bool isTranslating;
    private bool isPlayerRotating;
    public bool isMoving;
    public bool isAlive = true;

    [HideInInspector]
    public bool canMove = true;

    void Start()
    {
        Time.timeScale = 1f;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //da ne zaboravim da otkljucam kursor kada je potrebno
        Cursor.visible = false;
    }

    void Update()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpSpeed;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
            isTranslating = true;
        else
            isTranslating = false;

        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
            isPlayerRotating = true;
        else
            isPlayerRotating = false;

        if (isTranslating || isPlayerRotating || !characterController.isGrounded)
            isMoving = true;
        else
            isMoving = false;


        if (Input.GetKeyDown(KeyCode.R))
            ReloadLevel();

    }

    /*public void Die()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }*/

    private void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
