using UnityEngine;

public class kill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            health murder = other.GetComponent<health>();
            murder.Die();

        }
    }
}
