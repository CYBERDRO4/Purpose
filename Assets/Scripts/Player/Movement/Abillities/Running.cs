using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Running : Player
{
    private void Awake()
    {
        controls = new Player_Controls();
        controls.Main.Run.performed += context => Run();
        controls.Main.Run.canceled += context => Walk();
    }

    private void OnEnable() => controls.Enable();
    private void OnDisable()
    {
        controls.Disable();
        Walk();
    }

    public static Action<string> _RunOrWalk;

    public static void RunOrWalk(string _walkorrun)
    {
        if (_RunOrWalk != null)
            _RunOrWalk.Invoke(_walkorrun);
    }
    private void Run()
    {
        RunOrWalk("run");
    }
    private void Walk()
    {
        RunOrWalk("walk");
    }

}
