using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 1;
    public int currentHealth;

    public int damage = 1;

    public float speed = 1;
    public float jumpHeight = 5;

    void Start()
    {
        speed *= Time.deltaTime;
        currentHealth = maxHealth;
    }

    void Update()
    {
        
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }
}
