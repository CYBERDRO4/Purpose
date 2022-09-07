using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ToolData", menuName = "Data/Tool Data", order = 51)]
public class ToolData : ScriptableObject
{
    [SerializeField] private int toolId;
    [SerializeField] private string toolName;
    [SerializeField] private Sprite icon;

    public int GetToolID()
    {
        return toolId;
    }
    public string GetToolName()
    {
        return toolName;
    }
    public Sprite GetToolIcon()
    {
        return icon;
    }
}
