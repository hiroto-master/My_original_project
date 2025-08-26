using System;
using UnityEngine;

public class EnemyKiller : MonoBehaviour
{
    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            other.gameObject.transform.position = new Vector3(0,0,0);
        }
    }
}
