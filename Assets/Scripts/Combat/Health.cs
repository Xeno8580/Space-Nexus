using System;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;
    public UnityEvent onDeath;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake() => currentHealth = maxHealth;
    
        
    public void TakeDamage(int amount)
    {
        currentHealth = Mathf.Max(currentHealth - amount, 0);
        if (currentHealth == 0) Die();
    }

    // Update is called once per frame
    void Die()
    {
        onDeath?.Invoke();
        Destroy(gameObject);
    }
}
