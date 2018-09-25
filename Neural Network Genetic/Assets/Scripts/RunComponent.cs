using UnityEngine;

public class RunComponent : NeuralNetworkNodeMonoBehaviour, INNResetable
{
    [SerializeField]
    protected float m_StartSpeed = 1f;
    [SerializeField]
    protected float m_AccelerationMultiplier = 0.1f;
    [SerializeField]
    protected float minimumSpeed = 0.4f;
    public float m_MoveSpeed;

    private void Awake()
    {
        NNReset();
    }

    void FixedUpdate()
    {
        transform.position += transform.right * m_MoveSpeed;
    }

    public void NNReset()
    {
        m_MoveSpeed = m_StartSpeed;
    }
}