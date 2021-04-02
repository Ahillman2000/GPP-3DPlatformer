using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    EnemyStats enemyStatsScript;

    void Start()
    {
        enemyStatsScript = this.GetComponent<EnemyStats>();
    }

    void Update()
    {
        if(enemyStatsScript.currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
