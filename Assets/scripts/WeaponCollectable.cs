using UnityEngine;

public class WeaponCollectable : MonoBehaviour 
{
    public Weapon collectablegunPrefab;
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
            WeaponHandler weaponHandler = other.GetComponent<WeaponHandler>();
            if (weaponHandler != null )
            {
                Weapon weapon = GameObject.Instantiate(collectablegunPrefab);
                weaponHandler.Equip(weapon);

                Destroy(gameObject);
            }
        }
    }
}
