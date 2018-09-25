using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunOutputNode : NeuralNetworkNodeMonoBehaviour, INeuralNetworkOutputNode, INNFitnessEvaluator {

    public Collider2D Collider;
    public float shootCoolDown = 5f;
    private float lastShootTimeStamp;
    public GameObject bulletPrefab;
    private float hits;

    public void Activation(float activationValue)
    {
        if (lastShootTimeStamp + shootCoolDown < Time.time)
        {
            lastShootTimeStamp = Time.time;
            var bulletGmj = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bulletGmj.GetComponent<Owned>().SetOwner(Collider);
            bulletGmj.GetComponent<CallbackOnCollisionComponent>().onCollisionEvent += delegate (Collision2D collision) { BulletCollisionEvent(collision, bulletGmj); };
        }
    }

    private void BulletCollisionEvent(Collision2D collision, GameObject bulletGmj)
    {
        if (collision.gameObject.GetComponent<NeuralNetwork>() != null)
        {
            hits++;
            var health = collision.gameObject.GetComponent<HealthComponent>();
            health.Damage();
        }
        Destroy(bulletGmj);
    }

    public float GetFitness()
    {
        return hits;
    }
}
