using System;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float damage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<HealthComponent>().ReceiveDamage(damage);
    }
}
