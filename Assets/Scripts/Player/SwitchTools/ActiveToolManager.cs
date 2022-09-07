using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToolManager : MonoBehaviour
{
    private GameObject Arms;
    private GameObject Hook;

    public static Action<int> ActiveToolId;


    public static void SendActiveToolId(int activeToolId)
    {
        if (ActiveToolId != null)
            ActiveToolId.Invoke(activeToolId);
    }

    private void OnDestroy()
    {
        ToolList.ActiveTool -= CheckActiveTool;
    }

    private int toolId;
    private ToolData activeToolData;
    private void Awake()
    {
        ToolList.ActiveTool += CheckActiveTool;
        foreach (Transform child in transform)
        {
            switch (child.GetComponent<Tool>().GetThisToolId())
            {
                case 0:
                    Arms = child.gameObject;
                    Debug.Log(Arms);
                    break;
                case 1:
                    Hook = child.gameObject;
                    Debug.Log(Hook);
                    break;
            }
        }
    }

    private void DeactivateAll()
    {
        Arms.SetActive(false);
        Hook.SetActive(false);
    }

    private void CheckActiveTool(ToolData activeTool)
    {
        activeToolData = activeTool;
        DeactivateAll();
        switch (activeToolData.GetToolID())
        {
            case 0:
                toolId = 0;
                Arms.SetActive(true);
                break;
            case 1:
                toolId = 1;
                Hook.SetActive(true);
                break;
        }
        SendActiveToolId(toolId);
        Debug.Log(activeToolData.GetToolID());
    }
}
