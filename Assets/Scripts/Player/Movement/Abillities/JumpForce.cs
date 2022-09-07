using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    [SerializeField] private float jumpMultiplier;


    public static float _jumpForce { get; private set; }

    private void FixedUpdate()
    {
        _jumpForce += Time.deltaTime * jumpMultiplier;
    }
    private void OnEnable()
    {
        _jumpForce = 0;
    }
    private void OnDisable()
    {
        _jumpForce = 0;
    }

}
