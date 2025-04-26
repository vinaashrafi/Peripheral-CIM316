using System;
using UnityEngine;

[Serializable]
public class circut
{
    public LightPowerManager[] circutPowerables;
    public int circutPowerStates;
}
public class PowerManager : MonoBehaviour
{
    public circut[] powerCircuts;

    #region Breaker Update functions from Lightswitch
    public void UpdateBreaker(int breakerIndex, int powerChangeValue)
    {
        powerCircuts[breakerIndex].circutPowerStates = powerChangeValue;
        if (powerCircuts[breakerIndex].circutPowerStates > 0)
        {
            TurnOnSpecificBreaker(powerCircuts[breakerIndex]);
        }
        else
        {
            TurnOffSpecificBreaker(powerCircuts[breakerIndex]);
        }
    }
    public void TurnOffSpecificBreaker(circut specificPowerCircut)
    {
        Debug.Log("I am turning offf");
        foreach (var Powerable in specificPowerCircut.circutPowerables)
        {
            Powerable.TurnoffLight();
        }
    }
    public void TurnOnSpecificBreaker(circut specificPowerCircut)
    {
        Debug.Log("I am turning onnn");
        foreach (var Powerable in specificPowerCircut.circutPowerables)
        {
            Powerable.TurnonLight();
        }
    }
    #endregion
    
    public void PowerShutdown(circut[] PowerCircuts)
    {
        foreach (var Powerable in PowerCircuts)
        {
          //  Powerable.circutPowerables;
        }
    }

    public void PowerOn()
    {
        
    }
}
