using System;
using UnityEngine;

public class LightPowerManager : MonoBehaviour
{
    public Light lightSource;
    public float savedPower;

    private void Awake()
    {
        savedPower = lightSource.intensity;
    }

    public void TurnoffLight()
    {
        lightSource.intensity = 0;
    }
    public void TurnonLight()
    {
        lightSource.intensity = savedPower;
    }
}
