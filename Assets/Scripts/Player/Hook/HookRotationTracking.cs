using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRotationTracking : MonoBehaviour
{
    private Vector3 mousePos;
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(MouseCheck.GetMousePosition());
        mousePos.z = 0;
        transform.up = -(transform.position - mousePos).normalized;
    }
}
