using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera cam;
    public float damage = 100;
    public float attackCooldown = 0;
    public float attackDelay = 0;
    public GameObject target;

    private bool canFire = true;

    public void Fire()
    {

        Debug.Log("test");
        if (!canFire)
            return;

        canFire = true;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            health hithealth = hit.transform.parent.GetComponent<health>();
            if (hithealth != null)
            {
                hithealth.Damage(100);
                
            }
        }
    }
    

    
  
}
