using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManagerController : MonoBehaviour
{
    [Header("SubManagers")]

    [SerializeField] private SubManagerData ArmsSubManager;
    [SerializeField] private SubManagerData HookSubManager;

    [Header("ActiveSubManager")]
    [SerializeReference] private SubManagerData activeSubManager;
    public SubManagerData GetActiveAbilitySubManager() => activeSubManager;


    [Header("Ability Objects")]
    [SerializeField] private GameObject tool;
    public GameObject GetTool() => tool;


    [SerializeField] private GameObject push;
    public GameObject GetPush() => push;


    [SerializeField] private GameObject lightbulb;
    public GameObject GetLightbulb() => lightbulb;


    private AbilityManager am;

    private void Awake()
    {
        ActiveToolManager.ActiveToolId += ToolsController;
        am = gameObject.AddComponent<AbilityManager>();
    }

    private void ToolsController(int toolId)
    {
        switch (toolId)
        {
            case 0:
                activeSubManager = ArmsSubManager;
                am.DisableAll();
                am.SpringsControllerOn();
                am.RunningControllerOn();
                am.PlayerReversalControllerOn();
                am.PushingControllerOn();
                am.LightbulbControllerOn();
                break;
            case 1:
                activeSubManager = HookSubManager;
                am.DisableAll();
                am.SpringsControllerOn();
                am.PlayerReversalControllerOn();
                break;
        }
    }

}
