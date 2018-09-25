using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyGameObject))]
public class DestroyAfterTime : MonoBehaviour {

    private DestroyGameObject destroyGameObject;
    private float timeStamp;
    [SerializeField]
    private float killDuration = 10f;
    [SerializeField]
    private float maxRandomDuration = 3f;
    private float randomDuration;

    void Start () {
        destroyGameObject = GetComponent<DestroyGameObject>();
        timeStamp = Time.time;
        randomDuration = Random.Range(0, maxRandomDuration);
    }
	
	void Update () {
		if(Time.time > timeStamp + killDuration + randomDuration)
        {
            destroyGameObject.Trigger();
        }
    }
}
