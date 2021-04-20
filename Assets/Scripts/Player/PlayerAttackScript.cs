using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    //CharacterAnimatorController characterAnimatorController;

    public LayerMask enemyLayer;

    public GameObject attackPoint;
    Transform hitDetection;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    void Start()
    {
        hitDetection = attackPoint.transform;

        attackPoint.SetActive(false);
    }

    void Attack()
    {
        if (attackPoint.activeInHierarchy)
        {
            Collider[] hitEnemies = Physics.OverlapSphere(hitDetection.position, attackRange, enemyLayer);

            foreach (Collider enemy in hitEnemies)
            {
                Debug.Log("Enemy hit");
                enemy.GetComponent<EnemyStats>().TakeDamage(attackDamage);
                attackPoint.SetActive(false);
            }
        }
    }

    void DebugAttackpoint()
    {
        if(attackPoint.activeInHierarchy)
        {
            Debug.Log("attack point active");
        }
        else
        {
            Debug.Log("attack point deactive");
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            attackPoint.SetActive(true);
        }

        //DebugAttackpoint();

        Attack();
    }
}

/*public class PlayerAttackScript : MonoBehaviour
{
    //CharacterAnimatorController characterAnimatorController;
    Animator anim;
    
    public LayerMask enemyLayer;

    public Transform attackPoint;

    public float attackRange = 0.5f;
    public int attackDamage = 1;

    // Start is called before the first frame update
    void Start()
    {
        //characterAnimatorController = this.GetComponent<CharacterAnimatorController>();
        anim = this.GetComponent<Animator>();
    }

    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.position, attackRange, enemyLayer);

        foreach(Collider enemy in hitEnemies)
        {
            Debug.Log("Enemy hit");
            enemy.GetComponent<EnemyStats>().TakeDamage(attackDamage);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }
}*/