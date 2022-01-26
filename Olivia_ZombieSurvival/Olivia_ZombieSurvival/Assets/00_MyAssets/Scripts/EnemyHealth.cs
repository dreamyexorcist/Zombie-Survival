using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyHealth = 100f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        enemyHealth -= damage;

        if(enemyHealth <= 0)
        {
            GetComponent<Animator>().SetTrigger("Dead");
            GetComponent<NavMeshAgent>().speed = 0;
            Destroy(gameObject,2f);       
        }
    }
}
