using UnityEngine;

[RequireComponent(typeof(FitnessDistanceSensor))]
public class NNINDistanceEvaluator : MonoBehaviour, INeuralNetworkInputNode
{
    private FitnessDistanceSensor m_DistanceSensor;

    private void Awake()
    {
        m_DistanceSensor = GetComponent<FitnessDistanceSensor>();
    }

    public float ComputeActivation()
    {
        return MathLibrary.Clamb(m_DistanceSensor.GetDistanceToWall());
    }
}