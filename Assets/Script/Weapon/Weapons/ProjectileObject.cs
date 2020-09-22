using UnityEngine;

public class ProjectileObject : MonoBehaviour
{
    public WeaponProperties bulletProperties;
    IDamageable _damageableObject;
    Rigidbody2D _bulletRigidbody;
    bool isPlayerBullet;

    private void OnTriggerEnter2D(Collider2D other)     //If the bullet hits something with "IDamageble"
    {
        _damageableObject = other.GetComponent<IDamageable>();
        if (_damageableObject != null)
        {
            _damageableObject.TakenDamge(bulletProperties.damage, 
                bulletProperties.multiplierPoints, true);
            Destroy(gameObject);
        }
    }

    public void IsPlayerBullet(bool playerBullet)
    {
        isPlayerBullet = playerBullet;
        _bulletRigidbody = GetComponent<Rigidbody2D>();
        Destroy(gameObject, bulletProperties.survivaltime);
        _bulletRigidbody.gravityScale = 0;

        if (playerBullet)
        {
            _bulletRigidbody.AddForce(transform.up * bulletProperties.bulletSpeed);     //Playerbullet
            gameObject.layer = 0;
        }

        else if(!playerBullet)
        {
            _bulletRigidbody.AddForce(-transform.up * bulletProperties.bulletSpeed);     //Enemybullet
            gameObject.layer = 8;
        }
    }
}
