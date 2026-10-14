using UnityEngine;

public class playerdetection : MonoBehaviour
{
    [SerializeField] private GameObject enemyAI;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enemymelee enemystab = enemyAI.GetComponent<enemymelee>();
            if (enemystab != null)
            {
                enemystab.target = other.gameObject;
            }
        }
    }
}
