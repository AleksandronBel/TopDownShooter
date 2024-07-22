using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public Action<int> OnHealthChanged;

    public void TakeDamageFromBullet(int damage) => OnHealthChanged?.Invoke(damage);
}
