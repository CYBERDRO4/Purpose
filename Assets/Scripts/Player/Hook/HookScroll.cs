using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScroll : Player
{
    [SerializeField] private GameObject hookTip;
    [SerializeField] private float scrollMultiplier;
    public float ropeLength { get; private set; }
    private float maxRopeLength;
    private float minRopeLength;

    private bool isHolded;

    private GameObject hookObj;

    private float scrollInput;

    private void Awake()
    {
        controls = new Player_Controls();
        HookHoldCheck.HookIsHold += ChangeRopeLength;
    }

    private void Start()
    {
        maxRopeLength = HookShot.publicShotDistance;
        minRopeLength = 0.005f;
        ropeLength = maxRopeLength;
    }

    private void OnDisable()
    {
        controls.Disable();
        HookHoldCheck.HookIsHold -= ChangeRopeLength;
    }

    private void ChangeRopeLength(Collision2D hookObjectCollision)
    {
        isHolded = true;
        hookObj = hookObjectCollision.gameObject;
        ropeLength = Vector2.Distance(gameObject.transform.position, hookTip.transform.position);
        Debug.Log("прицепился " + ropeLength);
    }

    private void OnEnable()
    {
        controls.Enable();
        HookHoldCheck.HookIsHold += ChangeRopeLength;
        ropeLength = maxRopeLength;
    }
    private void Update()
    {
        if(isHolded == true && hookObj.GetComponent<Rigidbody2D>() != null)
        {
            if(hookObj.GetComponent<Rigidbody2D>().mass > gameObject.transform.parent.parent.GetComponent<Rigidbody2D>().mass)
            {
                if (GroundCheck.isGrounded)
                {
                    maxRopeLength = Vector2.Distance(gameObject.transform.position, hookTip.transform.position);
                    if (ropeLength > maxRopeLength)
                        ropeLength = maxRopeLength;
                }
                else
                    maxRopeLength = HookShot.publicShotDistance;
            }
        }
        ScrollLength();
        gameObject.transform.parent.parent.GetComponent<SpringJoint2D>().distance = ropeLength;
        hookTip.GetComponent<SpringJoint2D>().distance = ropeLength;
    }

    private void ScrollLength()
    {
        scrollInput = controls.Hook.HookScrolling.ReadValue<float>() / Mathf.Abs(controls.Hook.HookScrolling.ReadValue<float>());
        Debug.Log(scrollInput);
        if (ropeLength < maxRopeLength && scrollInput > 0)
            ropeLength += scrollMultiplier * scrollInput;
        if (scrollInput < 0 && ropeLength > minRopeLength)
            ropeLength += scrollMultiplier * scrollInput;
    }
}
