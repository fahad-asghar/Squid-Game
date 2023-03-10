using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Joystick joystick;
    [SerializeField] float moveSpeed;
    [SerializeField] float rotationFactor;

    public bool isMoving;
    public static PlayerMovement instance;

    private void Awake()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
        HandleAnimation();
    }

    private void HandleRotation()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, joystick.Horizontal * 90, 0);
        transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactor * Time.deltaTime);
    }

    private void HandleMovement()
    {
        if (joystick.Vertical > .5f || joystick.Vertical < -.5f
            || joystick.Horizontal > .5f || joystick.Horizontal < -.5f)
        {
            isMoving = true;
            transform.position +=
                 new Vector3(joystick.Horizontal * moveSpeed * Time.deltaTime, 0, Mathf.Abs(joystick.Vertical) * moveSpeed * Time.deltaTime);
        }
        else
            isMoving = false;
    }

    private void HandleAnimation()
    {
        if (joystick.Vertical > .5f || joystick.Vertical < -.5f
          || joystick.Horizontal > .5f || joystick.Horizontal < -.5f)
            animator.SetBool("IsRunning", true);      
        else
            animator.SetBool("IsRunning", false);
    }
}
