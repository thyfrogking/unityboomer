using UnityEngine;
using UnityEngine.AI;
public class enemymelee : MonoBehaviour
{
    public GameObject target;

    private NavMeshAgent agent;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        agent.destination = target.transform.position;
    }
}
