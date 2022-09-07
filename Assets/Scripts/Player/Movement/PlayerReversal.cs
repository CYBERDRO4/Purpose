using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReversal : MonoBehaviour
{
    public static bool isRight { get; private set; }
    public static bool isLeft { get; private set; }

    private void Start()
    {
        isRight = true;
    }
    void FixedUpdate()
    {
        if (PlayerController.moveInput > 0 && isRight == false)
        {
            isRight = true;
            isLeft = false;
            gameObject.transform.Rotate(0, 180, 0);
        }
        else if(PlayerController.moveInput < 0 && isLeft == false)
        {
            isLeft = true;
            isRight = false;
            gameObject.transform.Rotate(0, -180, 0);
        }
    }
}
