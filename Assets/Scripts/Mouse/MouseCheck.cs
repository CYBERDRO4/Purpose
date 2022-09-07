using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCheck : MonoBehaviour
{
    private static Vector3 mousePosition;

    public static Vector3 GetMousePosition()
    {
        mousePosition = Mouse.current.position.ReadValue();
        return mousePosition;
    }
}
