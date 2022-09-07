using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ToolList : MonoBehaviour
{
    private LinkedList<ToolData> tools = new LinkedList<ToolData>();

    private int toolListCount;

    private ToolData activeTool;

    public static Action<int> ChangeToolsCount;

    public static void SendChangedToolsCount(int toolListCount)
    {
        if (ChangeToolsCount != null)
            ChangeToolsCount.Invoke(toolListCount);
    }

    public static Action<ToolData> ActiveTool;

    public static void SendActive(ToolData activeTool)
    {
        if (ActiveTool != null)
            ActiveTool.Invoke(activeTool);
    }

    private void Awake()
    {
        ToolSwitcher.ActiveIndexForToolList += WhatIsActiveTool;
        Tool.AddTool += AddTool;
        Tool.RemoveTool += RemoveTool;
    }
    private void OnDestroy()
    {
        ToolSwitcher.ActiveIndexForToolList -= WhatIsActiveTool;
        Tool.AddTool -= AddTool;
        Tool.RemoveTool -= RemoveTool;
    }
    private void Start()
    {
        toolListCount = tools.Count();
        SendChangedToolsCount(toolListCount);
        activeTool = tools.ElementAt(0);
    }

    private void AddTool(ToolData tool)
    {
        tools.AddLast(tool);
        toolListCount = tools.Count();
        SendChangedToolsCount(toolListCount);
    }
    private void RemoveTool(ToolData tool)
    {
        if (tool != null)
        {
            tools.Remove(tools.Find(tool));
        }
        toolListCount = tools.Count();
        SendChangedToolsCount(toolListCount);
    }

    private void WhatIsActiveTool(int activeIndex)
    {
        activeTool = tools.ElementAt(activeIndex);
        SendActive(activeTool);
        Debug.Log(activeTool);
    }
}
