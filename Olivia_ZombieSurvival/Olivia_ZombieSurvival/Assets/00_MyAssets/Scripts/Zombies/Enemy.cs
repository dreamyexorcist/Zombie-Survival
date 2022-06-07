using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform aiTarget;
    [SerializeField] float range;
    [SerializeField] float attackDamage = 80f;
    NavMeshAgent navMeshAgent;

    private float distanceToAiTarget = 1000000f;

    public bool shouldAiAttack = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        distanceToAiTarget = Vector3.Distance(aiTarget.position, transform.position);

        if (shouldAiAttack == true)
        {
            EngagePlayer();
        }
        else if (distanceToAiTarget <= range)
        {
            shouldAiAttack = true;
        }

    }

    private void ChaseAiTarget()
    {
        GetComponent<Animator>().SetBool("Attack", false);
        GetComponent<Animator>().SetTrigger("Move");
        navMeshAgent.SetDestination(aiTarget.position);
    }

    private void AttackAiTarget()
    {
        GetComponent<Animator>().SetBool("Attack", true);        
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    private void EngagePlayer()
    {
        if (distanceToAiTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseAiTarget();
        }

        if (distanceToAiTarget <= navMeshAgent.stoppingDistance)
        {
            AttackAiTarget();
        }
    }

    public void DamageThePlayer()
    {
        if (aiTarget == null) { return; }
        aiTarget.GetComponent<FirstPersonController>().ApplyDamage(attackDamage);
                
    }
}
