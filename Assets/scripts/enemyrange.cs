using UnityEngine;
using UnityEngine.AI;
public class enemyrange : MonoBehaviour
{
    public GameObject target;
    public float attackReach = 4;
    public float attackDelay = 1;

    private NavMeshAgent agent;
    private float attackCooldown;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {


        if (attackCooldown > 0)
            attackCooldown -= Time.deltaTime;

        if (target != null)
        {

            if (Vector3.Distance(transform.position, target.transform.position) > attackReach)
            {
                agent.isStopped = false;
                agent.destination = target.transform.position;
            }
            else
            {
                agent.isStopped = true;
                Attack();
            }
        }
    }
    private void Attack()
    {
        if (attackCooldown <= 0)
        {
            health targetHealth = target.GetComponent<health>();
            if (targetHealth != null)
            {
                targetHealth.Damage(20);
            }
            attackCooldown = attackDelay;
        }
    }



}