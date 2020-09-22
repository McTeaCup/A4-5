using UnityEngine;

public class EnemyFire : MonoBehaviour
{
    public GameObject bullet;
    EnemyProperties enemyProperties;
    int amountOfBullets;
    float _nextTimeToFire;
    float maxAngle;
    float _finalMaxAngle;

    void Awake()
    {
        enemyProperties = transform.parent.GetComponent<EMovement>().enemyProperties;
    }

    // Update is called once per frame
    void Update()
    {
        #region Weapon parameters
        maxAngle = enemyProperties.bulletAngle;
        amountOfBullets = enemyProperties.bulletAmount;
        _finalMaxAngle = 57.33f * maxAngle;
        #endregion

        MultiShot();
    }

    void MultiShot()
    {

        if(Time.time > _nextTimeToFire)
        {
            _nextTimeToFire = Time.time + enemyProperties.fireRate;

            // convert spread to radians
            float _angSpread = _finalMaxAngle * Mathf.Deg2Rad;

            // angle between each bullet
            float _angBetweenBullets = amountOfBullets == 1 ? 0 : _angSpread / (amountOfBullets - 1f);

            // offset to center bullets
            float _angOffset = amountOfBullets == 1 ? 0 : _angSpread * 0.5f;    //If bullet is even or uneven

            for (int i = 0; i < amountOfBullets; i++)
            {
                // angle for this bullet
                float angle = _angBetweenBullets * i - _angOffset;

                GameObject _bulletClone = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y , transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle));
                _bulletClone.GetComponent<ProjectileObject>().IsPlayerBullet(false);
                Destroy(_bulletClone, 3f);
            }
        }
    }
}