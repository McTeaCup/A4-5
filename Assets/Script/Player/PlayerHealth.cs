using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Properties")]
    [SerializeField] PlayerProperties _playerProperties = default;

    [Header("Healthbar")]
    [SerializeField] Gradient _healthbarGradiant = default;
    [SerializeField] Slider _healthbar = default;
    [SerializeField] Image _barImage = default;
    float _playerHealth = default;

    [Header("Effects")]
    [SerializeField] GameObject _damageEffect = default;
    [SerializeField] EffectManager _pEffects = default;
    [SerializeField] EffectManager _gEffects = default;
    bool isSlowingDown = false;

    void Awake()
    {
        _playerHealth = _playerProperties.health;
        _healthbar.maxValue = _playerHealth;
        _healthbar.value = _playerHealth;
        _healthbar.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        if (Time.timeScale >= 1 && isSlowingDown)
        {
            _healthbar.gameObject.SetActive(false);
        }

        if (_playerHealth <= 0)
        {
            Death();
        }
    }

    //If player takes damage, slow time/updatehealthbar(color and size)
    public void TakenDamge(float damage, float pointAdded, bool slowTime)
    {
        //Spawn damageEffect
        GameObject _damageClone = Instantiate(_damageEffect);
        _damageClone.transform.position = transform.position;
       
        //Show healthbar
        _healthbar.gameObject.SetActive(true);
        _playerHealth -= damage;
        _healthbar.value -= damage;
        _barImage.color = _healthbarGradiant.Evaluate(_playerHealth / _playerProperties.health);
        
        //Start Effects
        _gEffects.events[0].Invoke();
        isSlowingDown = true;
    }

    void Death()
    {
        //Free mouse
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        
        //Disabels all effects
        Time.timeScale = 1;
        _pEffects.events[1].Invoke();
    }
}
