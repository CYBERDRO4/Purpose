using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightbulbTimer : MonoBehaviour
{
    public static float timer { get; private set; }
    private void OnEnable()
    {
        timer = 0;
    }
    private void OnDisable()
    {
        timer = 0;
    }
    private void FixedUpdate()
    {
        if (GroundCheck.isGrounded == false)
            gameObject.GetComponent<Lightbulb>().enabled = false;
        else if (GroundCheck.isGrounded == true && gameObject.GetComponent<Lightbulb>().enabled == false)
        {
            if (OverheatCore.isOverheat != true)
                gameObject.GetComponent<Lightbulb>().enabled = true;
            gameObject.GetComponent<LightbulbTimer>().enabled = false;
        }
        timer += Time.deltaTime;
        if (timer > 0.35f && Lightbulb.isLight == false)
        {
            gameObject.GetComponent<Lightbulb>().LightActivate();
        }
    }
}
