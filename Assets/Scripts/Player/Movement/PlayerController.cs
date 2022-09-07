using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Player
{
    [SerializeField] private float moveRate;
    [SerializeField] private float runMoveMultiplier;
    [SerializeField] private float airMoveMultiplier;

    private float signVelocity;
    private bool touchSomeObj;
    private float moveLimiter;

    [SerializeField] private GameEvent _OverheatDissipationIsStop;

    public static bool isRunning { get; private set; }
    public static bool isStopping { get; private set; }
    public static float moveInput { get; private set; }

    private void Awake()
    {
        touchSomeObj = false;
        controls = new Player_Controls();
        TypeOfSender += PrepareJump;
        Running._RunOrWalk += Walk;
        Running._RunOrWalk += Run;
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable()
    {
        controls.Disable();
        TypeOfSender -= PrepareJump;
        Running._RunOrWalk -= Walk;
        Running._RunOrWalk -= Run;
    }

    private void FixedUpdate()
    {
        if (isStopping == false)
            moveLimiter = 1;
        else
            moveLimiter = 0;
        Move();
        ClingRadius();
    }

    private void ClingRadius()
    {
        if (moveInput != 0 && Pushing.canCling == false && GroundCheck.isGrounded == false && touchSomeObj == true && isStopping == false)
        {
            signVelocity = moveInput;
            isStopping = true;
        }
        else if (touchSomeObj == false || GroundCheck.isGrounded == true || Mathf.Sign(signVelocity) != Mathf.Sign(moveInput) || Pushing.canCling == true)
        {
            isStopping = false;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        touchSomeObj = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveRate = moveRate / airMoveMultiplier;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        moveRate = moveRate * airMoveMultiplier;
        touchSomeObj = false;
    }

    private void Move()
    {
        if (canMove == true)
        {
            moveInput = controls.Main.Move.ReadValue<float>();
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(moveRate * moveInput * moveLimiter, GetComponent<Rigidbody2D>().velocity.y);
        }
    }

    private void Run(string _walkorrun)
    {
        if(GroundCheck.isGrounded == true && canMove == true && _walkorrun == "run")
        {
            isRunning = true;
            moveRate = moveRate * runMoveMultiplier;
        }
    }
    private void Walk(string _walkorrun)
    {
        if (canMove == true && isRunning == true && _walkorrun == "walk")
        {
            isRunning = false;
            moveRate = moveRate / runMoveMultiplier;
        }
    }

    private void PrepareJump(string type)
    {
        if (type == "pileuppowerjump")
        {
            Walk("walk");
            moveInput = 0;
        }
    }

    public void StopAllMoves()
    {
        Walk("walk");
        isStopping = true;
        canJump = false;
        canMove = false;
        moveInput = 0;
        gameObject.GetComponent<JumpForce>().enabled = false;
    }

    public void StartAllMoves()
    {
        isStopping = false;
        canJump = true;
        canMove = true;
    }

    public void InterruptMovement()
    {
        StartCoroutine(InterruptMovementRoutine());
    }
    private IEnumerator InterruptMovementRoutine()
    {
        StopAllMoves();
        yield return new WaitForSeconds(0.45f);
        StartAllMoves();
    }
}
