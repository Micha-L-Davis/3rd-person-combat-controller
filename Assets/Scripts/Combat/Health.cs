using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;
    private int _health;

    public bool IsDead => _health == 0;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void Damage(int damage)
    {
        if (IsDead) return;

        _health = Mathf.Max(_health - damage, 0);

        Debug.Log(_health);
    }
}
