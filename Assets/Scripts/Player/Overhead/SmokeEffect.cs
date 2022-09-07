using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEffect : MonoBehaviour
{
    [SerializeField] private GameEvent _StopSmoke;

    [System.Obsolete]

    private void Update()
    {
        gameObject.GetComponent<ParticleSystem>().startColor = new Color(GetComponent<ParticleSystem>().startColor.r, GetComponent<ParticleSystem>().startColor.g, GetComponent<ParticleSystem>().startColor.b, OverheatCore.overheatingOccupancy / 3);
        if (gameObject.GetComponent<ParticleSystem>().startColor.a == 0 && OverheatCore.isHeatsUp == false)
            _StopSmoke.Raise();
    }
}
