using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookHoldCheck : MonoBehaviour
{

    public static System.Action<Collision2D> HookIsHold;

    public static void SendHookObjectCollision(Collision2D hookObjectCollision)
    {
        if (HookIsHold != null)
            HookIsHold.Invoke(hookObjectCollision);
    }

    private void OnEnable()
    {
        hookIsHold = false;
    }

    public static bool hookIsHold { get; private set; }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hookIsHold == false)
        {
            hookIsHold = true;
            SendHookObjectCollision(collision);
        }

    }

    public void HookIsUnhold()
    {
        hookIsHold = false;
    }
}
