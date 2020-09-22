using System.Collections.Generic;
using UnityEngine;

public class WeaponFunction : MonoBehaviour
{
    [Header("Essentials")]
    public GameObject bullet;
    HUDManager _hud;
    PlayerProperties _playerProperties;
    PlayerInput _playerInput;
    ProjectileObject _bullletInformation;

    int _currentWeapon = default;
    int _bulletAmount = default;
    float _nextTimeToFire = default;
    float _finalMaxAngle = 57.33f;      //1 degree
    [Tooltip("How much the total spread should increese with depending on the amount of bullets the player can fire at the time")]
    [SerializeField] float _increeseAngleOnBulletAmount = 10f;

    [Header("Arsenel")]
    public List<GameObject> weapons;

    private void Awake()    //Find relevant objects
    {
        _hud = GameObject.Find("HUD Manager").GetComponent<HUDManager>();
        _playerProperties = transform.GetComponent<PMovement>().objectParameters;
        _playerInput = GetComponent<PlayerInput>();
        _bullletInformation = bullet.GetComponent<ProjectileObject>();
        AddNewWeapon(bullet);

    }
    private void Update()
    {
        FireWeapon();

        SwitchWeapon();
    }

    void FireWeapon()
    {
        //If player can fire, fire shot
        if (_playerInput.WeaponFire() && Time.time > _nextTimeToFire)
        {
            //resets firerate
            _nextTimeToFire = Time.time + _bullletInformation.bulletProperties.fireSpeed;
            
            // convert spread to radians
            float _angSpread = (_finalMaxAngle * (_bulletAmount + _increeseAngleOnBulletAmount)) * Mathf.Deg2Rad;

            // angle between each bullet
            float _angBetweenBullets = _bulletAmount == 1 ? 0 : _angSpread / (_bulletAmount - 1f);

            // offset to center bullets
            float _angOffset = _bulletAmount == 1 ? 0 : _angSpread * 0.5f;    //If bullet is even or uneven

            for (int i = 0; i < _bulletAmount; i++)
            {
                // angle for this bullet
                float angle = _angBetweenBullets * i - _angOffset;

                GameObject _bulletClone = Instantiate(bullet, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(transform.rotation.x, transform.rotation.y, angle));
                _bulletClone.GetComponent<ProjectileObject>().IsPlayerBullet(true);
                Destroy(_bulletClone, 3f);
            }
        }
    }

    public void AddNewWeapon(GameObject _newWeapon) //If player collids with itempickup add to list
    {
        weapons.Add(_newWeapon);
        _currentWeapon = weapons.Count;
        _bullletInformation = _newWeapon.GetComponent<ProjectileObject>();
        _nextTimeToFire = Time.time + _bullletInformation.bulletProperties.fireSpeed;
        _nextTimeToFire = 0;
        _hud.UpdateHUD(_bullletInformation.bulletProperties.weaponImage, _bullletInformation.bulletProperties.bulletColor,
            _bullletInformation.bulletProperties.itemName);
    }

    void SwitchWeapon() //If player switches weapon
    {
        if (_playerInput.WeaponSwitch() == 1 || _playerInput.WeaponSwitch() == -1)
        {
            if (weapons.Count > 1)  //If player has more than one weapon, switch weapon
            {
                _currentWeapon = weapons.Count + _playerInput.WeaponSwitch();

                if (_currentWeapon == 1)
                {
                    _currentWeapon = 1;
                }

                //If player tries to reach a weapon index above current, select the highest index
                else if (_currentWeapon > weapons.Count)
                {
                    _currentWeapon = weapons.Count;
                }

                //Update current weapon properties
                bullet = weapons[_currentWeapon - 1];
                _bullletInformation = bullet.GetComponent<ProjectileObject>();
                _nextTimeToFire = 0;
                _hud.UpdateHUD(_bullletInformation.bulletProperties.weaponImage, _bullletInformation.bulletProperties.bulletColor, _bullletInformation.bulletProperties.itemName);
            }

            //If player has less than 1, weapon do nothing
            else
            {
                bullet = weapons[0];
            }
        }
    }

    public int BulletMultiplier(int pointMultiplier) //Definds how many bullets the player fiers at the time
    {
        //If player shoots less than 10 bullets, add one more
        if (_bulletAmount < 10)
        {
            _bulletAmount = pointMultiplier + 1;
        }

        return _bulletAmount;
    }
}