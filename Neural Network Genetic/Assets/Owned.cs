using System;
using UnityEngine;

internal class Owned : MonoBehaviour
{
    internal void SetOwner(Collider2D collider)
    {
        Physics2D.IgnoreCollision(GetComponent<Collider2D>(), collider, true);
    }
}