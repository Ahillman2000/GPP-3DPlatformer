using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeDivideScript : MonoBehaviour
{
    EnemyStats enemyStatsScript;
    EnemyBehaviour enemyBehaviourScript;

    public GameObject smallerEnemy;
    public int numberOfSplitters = 2;

    void Start()
    {
        enemyStatsScript = this.GetComponent<EnemyStats>();
        enemyBehaviourScript = this.GetComponent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyStatsScript.currentHealth <= 0)
        {
            for (int i = 0; i < numberOfSplitters; i++)
            {
                Instantiate(smallerEnemy, this.transform.position, this.transform.rotation);
            }
            Destroy(this.gameObject);
        }
    }
}
