using UnityEngine;

public class AmmoCollectable : MonoBehaviour
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

            WeaponHandler currentammo = other.GetComponent<WeaponHandler>();
            currentammo.Ammo += 10;
            Destroy(gameObject);

        }
    }
}

