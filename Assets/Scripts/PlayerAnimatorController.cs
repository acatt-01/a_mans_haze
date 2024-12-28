using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{
    Animator animator;
    int isWalkingAsh, isWalkingLeftAsh, isWalkingRightAsh, isCrouchingAsh, isRunningAsh, isAttackingAsh, isGrabbingAsh;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // increases performance
        isWalkingAsh = Animator.StringToHash("isWalking");
        isWalkingLeftAsh = Animator.StringToHash("isWalkingLeft");
        isWalkingRightAsh = Animator.StringToHash("isWalkingRight");
        isRunningAsh = Animator.StringToHash("isRunning");
        isAttackingAsh = Animator.StringToHash("iAttacking");
        isCrouchingAsh = Animator.StringToHash("isCrouched");
        isGrabbingAsh = Animator.StringToHash("isGrabbing");
    }

    // Update is called once per frame
    void Update()
    {
        bool isWalking = animator.GetBool(isWalkingAsh);
        bool isWalkingLeft = animator.GetBool(isWalkingLeftAsh);
        bool isWalkingRight = animator.GetBool(isWalkingRightAsh);
        bool isRunning = animator.GetBool(isRunningAsh);
        bool isCrouching = animator.GetBool(isCrouchingAsh);
        bool isAttacking = animator.GetBool(isAttackingAsh);
        bool isGrabbing = animator.GetBool(isGrabbingAsh);

        bool forwardPressed = Input.GetKey(KeyCode.W);
        bool leftPressed = Input.GetKey(KeyCode.A);
        bool rightPressed = Input.GetKey(KeyCode.D);
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool crouchPressed = Input.GetKeyDown(KeyCode.X);
        bool attackPressed = Input.GetKeyDown(KeyCode.Tab);
        bool grabPressed = Input.GetKeyDown(KeyCode.E);

        // if player presses w while in idle
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingAsh, true);
        }

        // if player is not pressing w -> stop walking
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingAsh, false);
        }

        // if player is idle and moves left
        if (!isWalkingLeft && leftPressed)
        {
            animator.SetBool(isWalkingLeftAsh, true);
        }
        if(isWalkingLeft && !leftPressed)
        {
            animator.SetBool(isWalkingLeftAsh, false);
        }

        // if player is idle and moves right
        if (!isWalkingRight && rightPressed)
        {
            animator.SetBool(isWalkingRightAsh, true);
        }
        if(isWalkingRight && !rightPressed)
        {
            animator.SetBool(isWalkingRightAsh, false);
        }


        // if player is walking and not yet running and presses left shift
        if (!isRunning && (forwardPressed && runPressed))
        {
            animator.SetBool(isRunningAsh, true);
        }

        // if player is running and stops running or stops walking
        if (isRunning && (!forwardPressed || !runPressed))
        {
            animator.SetBool(isRunningAsh, false);
        }

        // is in idle and crouch is pressed
        if (!isCrouching && crouchPressed)
        {
            animator.SetBool(isCrouchingAsh, true);
        }

        // gets out of crouch mode
        if (isCrouching && crouchPressed)
        {
            animator.SetBool(isCrouchingAsh, false);
        }

        if (!isAttacking && attackPressed)
        {
            animator.SetBool(isAttackingAsh, true);
        }

        if (isAttacking && !attackPressed)
        {
            animator.SetBool(isAttackingAsh, false);
        }

        if (!isGrabbing && grabPressed)
        {
            animator.SetBool(isGrabbingAsh, true);
        }

        if (isGrabbing && !grabPressed)
        {
            animator.SetBool(isGrabbingAsh, false);
        }
    }
}
