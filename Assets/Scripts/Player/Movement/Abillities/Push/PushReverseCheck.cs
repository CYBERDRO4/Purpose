using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushReverseCheck : MonoBehaviour
{
    public static float punchmoveiInput { get; private set; }


    void FixedUpdate()
    {
        if (Pushing.isPushing == true)
        {
            punchmoveiInput = PlayerController.moveInput;
            if (Pushing.pushingRight == true)
            {
                punchmoveiInput = punchmoveiInput;
            }
            else if (Pushing.pushingLeft == true)
            {
                punchmoveiInput = punchmoveiInput * -1;
            }
        }
        else if(Pushing.isPushing == false)
        {
            gameObject.GetComponent<PushReverseCheck>().enabled = false;
        }
    }
}
