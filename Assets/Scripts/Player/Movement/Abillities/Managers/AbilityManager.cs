using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    protected AbilityManagerController amController;

    private void Awake()
    {
        amController = gameObject.GetComponent<AbilityManagerController>();
    }

    public void ToolSwitcherControllerOn() => amController.GetTool().GetComponent<ToolSwitcher>().enabled = true;
    public void ToolSwitcherControllerOff() => amController.GetTool().GetComponent<ToolSwitcher>().enabled = false;

    public void DisableAll()
    {
        SpringsControllerOff();
        RunningControllerOff();
        PlayerReversalControllerOff();
        PushingControllerOff();
        LightbulbControllerOff();
    }
    
    public void SpringsControllerOn()
    {
        if (amController.GetActiveAbilitySubManager().canUseSprings)
            gameObject.transform.parent.GetComponent<Springs>().enabled = true;
    }
    public void SpringsControllerOff() => gameObject.transform.parent.GetComponent<Springs>().enabled = false;

    public void RunningControllerOn()
    {
        if (amController.GetActiveAbilitySubManager().canUseRun)
            gameObject.transform.parent.GetComponent<Running>().enabled = true;
    }
    public void RunningControllerOff() => gameObject.transform.parent.GetComponent<Running>().enabled = false;

    public void PlayerReversalControllerOn()
    {
        if (amController.GetActiveAbilitySubManager().canUseReversal)
            gameObject.transform.parent.GetComponent<PlayerReversal>().enabled = true;
    }
    public void PlayerReversalControllerOff() => gameObject.transform.parent.GetComponent<PlayerReversal>().enabled = false;
    
    public void PushingControllerOn()
    {
        if (amController.GetActiveAbilitySubManager().canUsePush)
            amController.GetPush().GetComponent<Pushing>().enabled = true;
    }
    public void PushingControllerOff() => amController.GetPush().GetComponent<Pushing>().enabled = false;
    
    public void LightbulbControllerOn()
    {
        if (amController.GetActiveAbilitySubManager().canUseLightbulb)
            amController.GetLightbulb().GetComponent<Lightbulb>().enabled = true;
    }
    public void LightbulbControllerOff() => amController.GetLightbulb().GetComponent<Lightbulb>().enabled = false;

}
