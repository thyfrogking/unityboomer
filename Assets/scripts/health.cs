using UnityEngine;


public class health : MonoBehaviour
{
    public float maxHealth = 100;

    public float Health;

    private void Awake()
    {
        Health = maxHealth;
    }

    public void Die()
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

