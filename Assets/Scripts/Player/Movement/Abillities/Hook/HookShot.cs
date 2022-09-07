using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookShot : Player
{
    [SerializeField] private float shotDistance;

    public static float publicShotDistance { get; private set; }



    [SerializeField] private float shotPower;
    [SerializeField] private Transform startPoint;


    [SerializeField] private GameEvent _HookIsUnhold;

    private Vector3 directionHook;

    private void Awake()
    {
        publicShotDistance = shotDistance;
        controls = new Player_Controls();
        controls.Hook.HookShot.performed += context => Shot();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Shot()
    {
        if (HookHoldCheck.hookIsHold == false)
        {
            gameObject.GetComponent<HookHoldCheck>().enabled = true;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            gameObject.GetComponent<SpringJoint2D>().distance = shotDistance;
            gameObject.GetComponent<Transform>().SetParent(null);
            ToDirectAVelocity(ShotDirection());
        }
        else if (HookHoldCheck.hookIsHold == true)
        {
            _HookIsUnhold.Raise();
            gameObject.GetComponent<HookHoldCheck>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            gameObject.GetComponent<Rigidbody2D>().freezeRotation = false;
            gameObject.GetComponent<Transform>().SetParent(null);
        }
    }
    private Vector2 ShotDirection()
    {
        directionHook = -(startPoint.position - Camera.main.ScreenToWorldPoint(MouseCheck.GetMousePosition())) / (startPoint.position - Camera.main.ScreenToWorldPoint(MouseCheck.GetMousePosition())).magnitude;
        return directionHook;
    }
    private void ToDirectAVelocity(Vector2 directionHook)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity += new Vector2(directionHook.x * shotPower, directionHook.y * shotPower);
    }
}
