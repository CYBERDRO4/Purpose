using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : Player
{
    private bool isTouched;
    public static bool pushingLeft { get; private set; }
    public static bool pushingRight { get; private set; }
    public static bool isPushing { get; private set; }
    public static bool isMissingPush { get; private set; }

    public static bool canCling { get; private set; }


    private bool objTypeIsGetting;
    public static RigidbodyType2D pushObjectType { get; private set; }

    [SerializeField] private GameEvent _InterruptMovement;
    [SerializeField] private GameEvent _StartPush;
    [SerializeField] private GameEvent _StopPush;
    private void Awake()
    {
        canCling = false;
        isPushing = false;
        isTouched = false;
        controls = new Player_Controls();
        controls.Main.Push.performed += context => PushOn();
    }

    private void OnEnable()
    {
        controls.Enable();
        gameObject.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnDisable()
    {
        controls.Disable();
        isPushing = false;
        canCling = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void PushOff()
    {
        _StopPush.Raise();
        objTypeIsGetting = false;
        canCling = true;
    }
    private void PushOn()
    {
        isMissingPush = false;
        isPushing = !isPushing;
        _InterruptMovement.Raise();
        if (isTouched == true && GroundCheck.isGrounded == true)
        {
            canCling = false;
            _StartPush.Raise();
            gameObject.GetComponent<TakingObjectPush>().enabled = true;
            pushingRight = PlayerReversal.isRight;
            pushingLeft = PlayerReversal.isLeft;
            gameObject.GetComponent<PushReverseCheck>().enabled = true;
        }
        else
        {
            _InterruptMovement.Raise();
            StartCoroutine(MissingPush());
        }
        if (isPushing == false)
            PushOff();
    }

    IEnumerator MissingPush()
    {
        isMissingPush = true;
        yield return new WaitForSeconds(0.45f);
        isPushing = false;
        _InterruptMovement.Raise();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && isPushing == false && isTouched == false)
        {
            if (objTypeIsGetting == false)
            {
                pushObjectType = collision.gameObject.GetComponent<Rigidbody2D>().bodyType;
                objTypeIsGetting = true;
            }
            isTouched = true;
        }
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && isTouched == true && isPushing == true)
        {
            isTouched = false;
            isPushing = false;
            PushOff();
        }
        canCling = true;
        Debug.Log(isTouched);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && collision.GetComponent<Rigidbody2D>().simulated == true && isPushing == false)
            isTouched = false;
        if (isPushing == false)
            objTypeIsGetting = false;
        if (isPushing == true)
        {
            isPushing = false;
            PushOff();
            _InterruptMovement.Raise();
            StartCoroutine(MissingPush());
        }
        canCling = false;
        Debug.Log(isTouched);
        if (isTouched == true)
            isTouched = false;
    }
}
