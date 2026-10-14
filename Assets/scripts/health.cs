using UnityEngine;


public class health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;

    [SerializeField] private float Health;

    private void Awake()
    {
        Health = maxHealth;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
    public void Damage(float amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            Die();
        }
    }
}

