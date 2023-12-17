using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{
    EnemyContext _ctx;

    private void Awake()
    {
        _ctx = GetComponentInParent<EnemyContext>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _ctx.IsInRange = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _ctx.IsInRange = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _ctx.Target = collision.gameObject;
            _ctx.IsInRange = true;
        }
    }
}
