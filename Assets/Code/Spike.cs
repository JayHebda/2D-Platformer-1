using System;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float damage = 1;

    void Start() { }

    void Update() { }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<HealthComponent>().ReceiveDamage(damage);
        }
    }
}