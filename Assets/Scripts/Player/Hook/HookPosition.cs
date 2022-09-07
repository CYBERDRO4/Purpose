using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookPosition : MonoBehaviour
{
    //это костыль, сделанный для того, чтобы крюк не выглядел так криво из-за анимации спрайтами.

    private bool isJumping;
    private bool canPositionHook;

    private void Update()
    {
        if(canPositionHook && GroundCheck.isGrounded && isJumping == true && gameObject.GetComponent<Transform>().localPosition.x < -1.07f && gameObject.GetComponent<Transform>().localPosition.y > 0.3f)
        {
            gameObject.GetComponent<Transform>().localPosition -= new Vector3(-0.01783f, 0.005f, 0);
        }
        if (GroundCheck.isGrounded && isJumping == false)
        {
            gameObject.GetComponent<Transform>().localPosition = new Vector3(-1.68f, 0.5f, 0);
            canPositionHook = false;
        }
        if (GroundCheck.isGrounded == false)
            gameObject.GetComponent<Transform>().localPosition = new Vector3(-1.07f, 0.3f, 0);
    }
    public void JumpHookPosition()
    {
        StartCoroutine(IsJumping());
        isJumping = true;
    }
    public void StandartHookPosition()
    {
        isJumping = false;
    }
    IEnumerator IsJumping()
    {
        yield return new WaitForSeconds(0.1f);
        canPositionHook = true;
    }
}
