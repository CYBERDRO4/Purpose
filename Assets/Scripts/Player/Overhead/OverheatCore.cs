using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverheatCore : MonoBehaviour
{
    [SerializeField] private float springsprolongedHeatingMultiplier;
    [SerializeField] private float flashAddOverrideCount;

    [SerializeField] private float overheatingOccupancyMax;

    [SerializeField] private float springsCoolingDownMultiplier;
    [SerializeField] private float flashCoolingDownMultiplier;
    [SerializeField] private float overheatCoolingDownMultiplier;

    public static float overheatingOccupancy { get; private set; }

    private float prolongedHeatingMultiplier;
    private float coolingDownMultiplier;

    [SerializeField] private GameEvent _Overheat;
    [SerializeField] private GameEvent _DisableAbilities;
    [SerializeField] private GameEvent _EnableAbilities;
    [SerializeField] private GameEvent _OverheatsEnd;
    [SerializeField] private GameEvent _StartSmoke;
    [SerializeField] private GameEvent _DisableOverheat;

    public static bool isHeatsUp { get; private set; }
    public static bool isOverheat { get; private set; }
    private void Awake()
    {
        Player.TypeOfSender += OverheatAdd;
        isHeatsUp = false;
        isOverheat = false;
    }
    private void OnDestroy()
    {
        Player.TypeOfSender -= OverheatAdd;
    }

    private void FixedUpdate()
    {
        if (isHeatsUp == true || (isHeatsUp == true && overheatingOccupancy < overheatingOccupancyMax))
            overheatingOccupancy += prolongedHeatingMultiplier * Time.deltaTime;
        if (overheatingOccupancy > 0 && isHeatsUp == false)
            overheatingOccupancy -= coolingDownMultiplier * Time.deltaTime; ;
        if (overheatingOccupancy > overheatingOccupancyMax)
        {
            overheatingOccupancy = overheatingOccupancyMax;
        }
        if (overheatingOccupancy >= overheatingOccupancyMax && isOverheat == false)
            Overheat();
        if (overheatingOccupancy <= 0 && isOverheat == true)
        {
            isOverheat = false;
            _OverheatsEnd.Raise();
            _EnableAbilities.Raise();
            coolingDownMultiplier = springsCoolingDownMultiplier;
        }
        if (overheatingOccupancy < 0 && isOverheat == false && isHeatsUp == false)
        {
            overheatingOccupancy = 0;
            _DisableOverheat.Raise();
        }
    }

    public void OverheatAdd(string _typeofsender)
    {
        switch (_typeofsender)
        {
            case "pileuppowerjump":
                prolongedHeatingMultiplier = springsprolongedHeatingMultiplier;
                coolingDownMultiplier = springsCoolingDownMultiplier;
                isHeatsUp = true;
                _StartSmoke.Raise();
                break;
            case "flash":
                coolingDownMultiplier = flashCoolingDownMultiplier;
                overheatingOccupancy += flashAddOverrideCount;
                _StartSmoke.Raise();
                break;
        }
    }
    private void Overheat()
    {
        coolingDownMultiplier = overheatCoolingDownMultiplier;
        isOverheat = true;
        _Overheat.Raise();
        _DisableAbilities.Raise();
        CoolingDown();
    }

    private void CoolingDown()
    {
        isHeatsUp = false;
    }
    public void OverheatStop()
    {
        isHeatsUp = false;
    }


}
