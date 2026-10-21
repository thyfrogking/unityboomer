using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Transform weaponAnchor;
    public Camera mainCam;
    [SerializeField] private float maxAmmo = 30;

    [SerializeField] public int Ammo;

    private void Awake()
    {
        Ammo = 10;
    }

    private Weapon equippedWeapon;

    private void OnAttack()
    {

        if (equippedWeapon != null)
        {
            if (Ammo > 0)
            {
                Ammo = Ammo - 1;
                equippedWeapon.Fire();
            }
            else
            {
                Debug.Log("noammo");
            }
           
        }
    }


    
public void Equip(Weapon weapon)
    {
        equippedWeapon = weapon;
        weapon.transform.parent = weaponAnchor;
        weapon.transform.position = weaponAnchor.position;
        weapon.transform.rotation = weaponAnchor.rotation;
        weapon.cam = mainCam;
    }


}
