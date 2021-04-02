using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth = 1;
    public float currentHealth;

    public float damage = 1;

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
}
