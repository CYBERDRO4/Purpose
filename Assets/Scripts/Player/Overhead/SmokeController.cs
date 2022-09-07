using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeController : MonoBehaviour
{
    public void SmokeEnable()
    {
        gameObject.GetComponent<SmokeEffect>().enabled = true;
        gameObject.GetComponent<ParticleSystem>().Play();
    }
    public void SmokeDisable()
    {
        gameObject.GetComponent<SmokeEffect>().enabled = false;
        gameObject.GetComponent<ParticleSystem>().Stop();
    }
}
