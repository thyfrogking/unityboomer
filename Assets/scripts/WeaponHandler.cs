using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public Transform weaponAnchor;
    public Camera mainCam;

    private Weapon equippedWeapon;

    private void OnAttack()
    {
        if (equippedWeapon != null)
        {
            equippedWeapon.Fire();
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
