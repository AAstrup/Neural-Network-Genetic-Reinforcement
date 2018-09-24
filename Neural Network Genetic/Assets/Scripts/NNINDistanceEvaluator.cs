using UnityEngine;

[RequireComponent(typeof(DistanceSensor))]
public class NNINDistanceEvaluator : MonoBehaviour, INeuralNetworkInputNode
{
    private DistanceSensor m_DistanceSensor;

    private void Awake()
    {
        m_DistanceSensor = GetComponent<DistanceSensor>();
    }

    public float ComputeActivation()
    {
        return MathLibrary.Clamb(m_DistanceSensor.GetDistanceToWall());
    }
}