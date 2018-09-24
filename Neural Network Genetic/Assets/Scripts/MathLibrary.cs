using System;
using UnityEngine;

public class MathLibrary
{
    //public static float SimpleClamp(float x)
    //{
    //    if (x > 0)
    //        return Mathf.Min(x, 1);
    //    else
    //        return Mathf.Max(x, -1);
    //}

    public static float SigmoidAlternative(float x)
    {
        return (2 * (1 / (1 + Mathf.Exp(-x)))) - 1f;
    }

    public static float DReLU(float x)
    {
        return Mathf.Clamp(x,-1f,1f);// x < 0 ? 0 : x;
    }

    internal static float Clamb(float m_AccumilatedActivation)
    {
        return SigmoidAlternative(m_AccumilatedActivation);
    }
}