using UnityEngine;

public class HeallingCollectable : MonoBehaviour
{
    public float rotationSpeed = 90;
    public float yPositionOscillation = 0.1f;

    private float YstartPosition;

    private void Awake()
    {
        YstartPosition = transform.position.y;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        float newY = YstartPosition + Mathf.Sin(Time.time) * yPositionOscillation;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);


    }









    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {

            health currenthealth = other.GetComponent<health>();
            if (currenthealth.Health < currenthealth.maxHealth)
            {
                currenthealth.Health += 20;
                Destroy(gameObject);
                if (currenthealth.Health > currenthealth.maxHealth) ;
                {
                    currenthealth.Health = currenthealth.maxHealth;
                }

            }
            

        }
    }
}



