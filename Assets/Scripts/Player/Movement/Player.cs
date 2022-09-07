using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static Action<string> TypeOfSender;

    public static void SendOverheatsBoost(string _typeofsender)
    {
        if (TypeOfSender != null)
            TypeOfSender.Invoke(_typeofsender);
    }

    protected Player_Controls controls;
    protected  static bool canJump;
    protected static bool canMove;

    private void Awake()
    {
        canJump = true;
        canMove = true;
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GroundCheck.isGrounded == true)
            canJump = true;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if(GroundCheck.isGrounded == false)
            canJump = false;
    }

}
