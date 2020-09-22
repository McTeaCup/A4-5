using UnityEngine;

public class Pickup : MonoBehaviour
{
    public GameObject[] weapons;
    int weaponIndex;

    private void Awake()
    {
        weaponIndex = Random.Range(0, weapons.Length);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<WeaponFunction>() != null)
        {
            if(other.GetComponent<WeaponFunction>().bullet != weapons[weaponIndex])
            {
                WeaponFunction weapon = other.GetComponent<WeaponFunction>();
                weapon.bullet = weapons[weaponIndex];
                weapon.AddNewWeapon(weapons[weaponIndex]);
                Destroy(transform.parent.gameObject);
            }
        }
    }
}
