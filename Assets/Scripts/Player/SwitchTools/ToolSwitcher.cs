using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolSwitcher : MonoBehaviour
{

    public static Action<int> ActiveIndexForToolList;

    public static void SendActiveIndexForToolList(int activeIndex)
    {
        if (ActiveIndexForToolList != null)
            ActiveIndexForToolList.Invoke(activeIndex);
    }


    private Player_Controls controls;
    private float handTool;
    private int activeIndex;
    private int maxIndex;

    private void Awake()
    {
        controls = new Player_Controls();
        controls.Main.HandToolSwitch.performed += context => HandToolSwitch();
        ToolList.ChangeToolsCount += ChangeToolsCount;
    }
    private void Start()
    {
        activeIndex = 0;
        StartCoroutine(StartActiveIndexine());
    }
    private void OnDestroy() => ToolList.ChangeToolsCount -= ChangeToolsCount;
    private void OnEnable() => controls.Enable();
    private void OnDisable() => controls.Disable();

    private void ChangeToolsCount(int toolListCount) => maxIndex = toolListCount - 1;

    private void HandToolSwitch()
    {
        if (GroundCheck.isGrounded == true)
        {
            handTool = controls.Main.HandToolSwitch.ReadValue<float>();
            activeIndex += (int)handTool;
            if (activeIndex < 0)
                activeIndex = maxIndex;
            if (activeIndex > maxIndex)
                activeIndex = 0;
            SendActiveIndexForToolList(activeIndex);
        }
    }

    IEnumerator StartActiveIndexine()
    {
        yield return new WaitForFixedUpdate();
        SendActiveIndexForToolList(activeIndex);
    }
}
