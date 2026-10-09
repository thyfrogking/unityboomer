using UnityEngine;
using UnityEngine.AI;
public class enemymelee : MonoBehaviour
{
    public GameObject target;
    public float attackReach = 1;
    public float attackDelay = 1;
    private NavMeshAgent agent;
    private float attackCooldown;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) > attackReach)
        {
            agent.isStopped = false;
            agent.destination = target.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }
    
}
