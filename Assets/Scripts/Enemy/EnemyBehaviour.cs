using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    EnemyStats enemyStatsScript;

    enum EnemyState { patrolling, attacking, fleeing }
    EnemyState enemyState;
    
    GameObject player;
    Vector3 target;

    Vector3 patrolSpot;

    public float minX;
    public float maxX;
    public float minZ;
    public float maxZ;
     
    float waitTime;
    readonly float startWaitTime = 0;

    public float distanceFromPlayer = 3f;

    public float attackDistance = 15f;

    public float retreatDistance;

    void Start()
    {
        enemyStatsScript = this.GetComponent<EnemyStats>();

        player = GameObject.FindGameObjectWithTag("Player");

        enemyState = EnemyState.patrolling;

        waitTime = startWaitTime;
        patrolSpot = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
    }

    void Update()
    {
        Vector3 slimePosition = new Vector3(this.transform.position.x, 1, this.transform.position.z);
        target = new Vector3(player.transform.position.x, 1, player.transform.position.z);

        // if touching ground
        //this.GetComponent<Rigidbody>().AddForce(Vector3.up * enemyStatsScript.jumpHeight);

        if (enemyState == EnemyState.patrolling && Vector3.Distance(slimePosition, target) < attackDistance)
        {
            enemyState = EnemyState.attacking;
        }
        /*else if (enemyState == EnemyState.attacking && enemyStatsScript.currentHealth <= enemyStatsScript.maxHealth / 3)
        {
            enemyState = EnemyState.fleeing;
        }*/
        else if (Vector3.Distance(slimePosition, target) > attackDistance)
        {
            enemyState = EnemyState.patrolling;
        }


        if (enemyState == EnemyState.patrolling)
        {
            this.transform.position = Vector3.MoveTowards(slimePosition, patrolSpot, enemyStatsScript.speed);
            transform.LookAt(patrolSpot);

            if (Vector3.Distance(slimePosition, patrolSpot) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    patrolSpot = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        else if (enemyState == EnemyState.attacking)
        {
            transform.LookAt(target);

            if (Vector3.Distance(slimePosition, target) > distanceFromPlayer)
            {
                this.transform.position = Vector3.MoveTowards(slimePosition, target, enemyStatsScript.speed);
            }
        }
        else if (enemyState == EnemyState.fleeing)
        {
            this.transform.position = Vector3.MoveTowards(slimePosition, target, -enemyStatsScript.speed);
        }
    }
}
