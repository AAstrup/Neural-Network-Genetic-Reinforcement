using UnityEngine;

[RequireComponent(typeof(DestroyGameObject))]
public class HealthComponent : MonoBehaviour
{
    private DestroyGameObject destroyGameObject;
    public int health = 3;

    private void Start()
    {
        destroyGameObject = GetComponent<DestroyGameObject>();
    }

    public void Damage()
    {
        if(health-- <= 0)
        {
            destroyGameObject.Trigger();
        }
    }
}