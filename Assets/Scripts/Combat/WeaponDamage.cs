using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [SerializeField] private Collider _myCollider;
    private List<Collider> triggeredColliders = new List<Collider>();

    private void OnEnable()
    {
        triggeredColliders.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == _myCollider) return;

        if (triggeredColliders.Contains(other)) return;

        triggeredColliders.Add(other);

        if (other.TryGetComponent<Health>(out Health health))
        {
            health.Damage(10);
        }
    }
}
