using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitnessDistanceSensor : MonoBehaviour, INNFitnessEvaluator, INNResetable {
    [SerializeField]
    private float m_Distance = 1f;
    private float distanceToWallAccumilated;
    RaycastHit2D hit;
    [SerializeField]
    private LayerMask m_LayerMask;

    public float GetDistanceToWall()
    {
        hit = Physics2D.Raycast(transform.position, transform.right, m_Distance, m_LayerMask);
        float distance = 0f;
        if (hit.collider != null)
            distance = hit.distance;
        else
            distance = m_Distance;
        distanceToWallAccumilated += distance;
        return distance;
    }

    public float GetFitness()
    {
        return distanceToWallAccumilated;
    }

    public void NNReset()
    {
        distanceToWallAccumilated = 0f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * m_Distance);
    }
}
