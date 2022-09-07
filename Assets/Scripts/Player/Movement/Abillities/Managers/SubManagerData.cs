using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ToolData", menuName = "Data/SubManager Data", order = 53)]
public class SubManagerData : ScriptableObject
{
    public bool canUseSprings;

    public bool canUseRun;

    public bool canUseReversal;

    public bool canUsePush;

    public bool canUseLightbulb;
}
