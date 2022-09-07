using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightbulb : Player
{
    private bool canActivateLightbulb;
    [SerializeField] private GameEvent _EnableOverheat;
    [SerializeField] private GameEvent _StopAllMoves;
    [SerializeField] private GameEvent _StartAllMoves;

    [SerializeField] private GameEvent _StartLightbulb;
    [SerializeField] private GameEvent _StopLightbulb;
    public static bool prepareLightbulb { get; private set; }
    public static bool isFlash { get; private set; }
    public static bool isLight { get; private set; }
    private void Awake()
    {
        isLight = false;
        isFlash = false;
        canActivateLightbulb = true;
        controls = new Player_Controls();
        controls.Main.Lightbulb.performed += context => CanLight();
        controls.Main.Lightbulb.canceled += context => Flash();
    }


    private void OnEnable() => controls.Enable();
    private void OnDisable()
    {
        controls.Disable();
        if (OverheatCore.isOverheat != true && prepareLightbulb == true)
            _StopLightbulb.Raise();
        isLight = false;
        isFlash = false;
        prepareLightbulb = false;
    }

    private void CanLight()
    {
        if (canActivateLightbulb == true && GroundCheck.isGrounded == true)
        {
            _StartLightbulb.Raise();
            prepareLightbulb = true;
            gameObject.GetComponent<LightbulbTimer>().enabled = true;
        }
    }

    public void LightActivate()         //свет
    {
        isLight = true;
    }

    private void Flash()
    {
        if (GroundCheck.isGrounded == true)
        {
                if (LightbulbTimer.timer <= 0.35f)
            {
                _EnableOverheat.Raise();
                SendOverheatsBoost("flash");
                StartCoroutine(Flashoutine());
            }
            StartCoroutine(LightBulbEndine());
        }
    }


    IEnumerator Flashoutine()           //вспышка
    {
        yield return new WaitForFixedUpdate();

        if (OverheatCore.isOverheat != true)
        {
            _StopAllMoves.Raise();
            canActivateLightbulb = false;
            isFlash = true;
        }
    }
    IEnumerator LightBulbEndine()
    {
        _StopAllMoves.Raise();
        gameObject.GetComponent<LightbulbTimer>().enabled = false;
        yield return new WaitForSeconds(0.2f);
        if (isFlash == true)
        {
            isFlash = false;               //конец вспышки
        }
        else
            isLight = false;
        prepareLightbulb = false;
        yield return new WaitForSeconds(0.45f);
        if (OverheatCore.isOverheat != true)
        {
            _StartAllMoves.Raise();
            _StopLightbulb.Raise();
        }
        canActivateLightbulb = true;
    }
}
