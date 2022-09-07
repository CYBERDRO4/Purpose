using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        Push();
        Cling();
        Jump();
        Move();
        TypeOfMove();
        LightbulbCheck();
        Overhead();
    }

    private void LightbulbCheck()
    {
        anim.SetBool("prepareLightbulb", Lightbulb.prepareLightbulb);
        anim.SetBool("isFlash", Lightbulb.isFlash);
        anim.SetBool("isLight", Lightbulb.isLight);
    }

    private void Cling()
    {
        anim.SetBool("canCling", Pushing.canCling);
    }
    private void Push()
    {
        anim.SetBool("isPushing", Pushing.isPushing);
        anim.SetFloat("pushInput", PushReverseCheck.punchmoveiInput);
        anim.SetBool("pushIsMissed", Pushing.isMissingPush);
    }
    private void Jump()
    {
        anim.SetBool("isGrounded", GroundCheck.isGrounded);
        if (JumpForce._jumpForce != 0)
            anim.SetBool("jumpIsPreparing", true);
        else
            anim.SetBool("jumpIsPreparing", false);
        if (GroundCheck.isGrounded == false)
        {
            anim.SetBool("jumpIsPreparing", false);
        }
        anim.SetFloat("jumpVelocity", gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }

    private void Move()
    {
        if (PlayerController.moveInput != 0)
            anim.SetBool("isWalking", true);
        else
            anim.SetBool("isWalking", false);
    }

    private void TypeOfMove()
    {
        anim.SetBool("isRunning", PlayerController.isRunning);
    }

    private void Overhead()
    {
        anim.SetBool("isOverheat", OverheatCore.isOverheat);
    }
}
