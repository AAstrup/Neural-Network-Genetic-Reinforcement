using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InNodeAngleToTarget : MonoBehaviour, INeuralNetworkInputNode {

    [SerializeField]
    private Transform m_Target;
    [SerializeField]
    private NeuralNetwork parent;
    [SerializeField]
    private bool m_DebugPrint;

    public float ComputeActivation()
    {
        if (m_Target == null)
        {
            UpdateTarget();
        }

        if (m_Target == null)
        {
            Debug.Log("No target found");
            return 180f;
        }
        else
        {
            return Vector2.Angle(transform.right, m_Target.position - transform.position);
        }
    }

    private void UpdateTarget()
    {
        var debug = GameContext.instance.GetDependency<NeuralNetworkAgentCollection>();
        var targetGmj = debug.spawns.FirstOrDefault(x => x.gameObject != parent.gameObject);
        if (targetGmj != null)
            m_Target = targetGmj.transform;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
