using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    EnemyContext _ctx;

    private void Awake()
    {
        _ctx = GetComponentInParent<EnemyContext>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var obj = collision.gameObject.GetComponent<PlayerContext>();
            if (!obj.IsDead)
            {
                obj.TakeDamage(_ctx.attackDamage);
            }
        }
    }
}
