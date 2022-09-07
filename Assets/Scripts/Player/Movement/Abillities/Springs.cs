using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Springs : Player
{

    public GameEvent _OverheatPowerBoostIsStop;
    [SerializeField] private GameEvent _EnableOverheat;
    [SerializeField] private GameEvent _StartJump;
    [SerializeField] private GameEvent _StopJump;

    private void Awake()
    {
        controls = new Player_Controls();
        controls.Main.Jump.performed += context => PrepareJump();
        controls.Main.Jump.canceled += context => Jump();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable()
    {
        controls.Disable();
        StopPrepare();
    }

    private void PrepareJump()
    {
        if (canJump == true && OverheatCore.isOverheat == false)
        {
            _StartJump.Raise();
            _EnableOverheat.Raise();
            SendOverheatsBoost("pileuppowerjump");
            canMove = false;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
            gameObject.GetComponent<JumpForce>().enabled = true;
        }
    }

    private void StopPrepare()
    {
        gameObject.GetComponent<JumpForce>().enabled = false;
        canJump = false;
        _OverheatPowerBoostIsStop.Raise();
        canJump = true;
    }
    private void Jump()
    {
        if (canJump == true && OverheatCore.isOverheat == false)
        {
            _OverheatPowerBoostIsStop.Raise();
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, JumpForce._jumpForce);
            canMove = true;
            gameObject.GetComponent<JumpForce>().enabled = false;
            _StopJump.Raise();
        }
    }
}
