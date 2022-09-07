using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheatController : MonoBehaviour
{
    public void OverheatEnable()
    {
        gameObject.GetComponent<OverheatCore>().enabled = true;
    }
    public void OverheatDisable()
    {
        gameObject.GetComponent<OverheatCore>().enabled = false;
    }
}
