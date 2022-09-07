using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour
{
    [SerializeField] private ToolData tool;

    public int GetThisToolId()
    {
        return tool.GetToolID();
    }

    public static System.Action<ToolData> AddTool;

    public static void SendAddTool(ToolData tool)
    {
        if (AddTool != null)
            AddTool.Invoke(tool);
    }

    public static System.Action<ToolData> RemoveTool;
    public static void SendRemoveTool(ToolData tool)
    {
        if (RemoveTool != null)
            RemoveTool.Invoke(tool);
    }

    private void Start()
    {
        AddToolToList();
        Debug.Log(tool + " добавлено");
    }

    private void AddToolToList()
    {
        SendAddTool(tool);
    }

    private void RemoveToolFromList()
    {
        SendRemoveTool(tool);
    }

}
