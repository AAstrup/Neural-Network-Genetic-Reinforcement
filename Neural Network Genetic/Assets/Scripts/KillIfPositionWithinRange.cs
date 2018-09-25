using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyGameObject))]
public class KillIfPositionWithinRange : MonoBehaviour {

    private DestroyGameObject nNResetable;
    private int m_Frame;
    public int _KillTickRate = 120;
    private Vector3 lastPost;
    private float killDistance = 5f;
    private Vector3 startPosition;

    private void Start()
    {
        nNResetable = GetComponent<DestroyGameObject>();
        lastPost = transform.position;
        startPosition = transform.position;
    }

    void FixedUpdate () {
		if(++m_Frame > _KillTickRate)
        {
            m_Frame = 0;
            if (Vector2.Distance(transform.position, lastPost) < killDistance)
            {
                nNResetable.Trigger();
                transform.position = startPosition;
            }
        }
	}
}
