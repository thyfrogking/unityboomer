using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Camera cam;
    public float damage = 1;

    private bool canFire = true;

    public void Fire()
    {
        if (!canFire)
            return;

        canFire = false;

        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            health hithealth = hit.transform.parent.GetComponent<health>();
            if (hithealth != null)
            {
                hithealth.Damage(damage);
            }
        }
    }
}
