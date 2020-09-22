using UnityEngine;

public class ShieldAbility : MonoBehaviour, IAbility
{
    [SerializeField] float _shieldRadius = 0.7f;
    [SerializeField] float _shieldDamage = 0.05f;
    [SerializeField] float _shieldSurvivalTime = 3f;
    [SerializeField] float _timeBeforeDamage = 0.5f;
    [SerializeField] float _damagePerTick = 0.5f;

    //Timer Peremeters
    float _waitBeforeDamage;
    float _countdownTime;
    Collider2D collidingObjects;

    private void Awake()
    {
        _waitBeforeDamage = 0;
    }

    void Update()
    {
        StartAbility();

        _countdownTime += Time.deltaTime;

        if (_countdownTime > _shieldSurvivalTime)
        {
            StopAbility();
        }
    }

    public void StartAbility()
    {
        collidingObjects = Physics2D.OverlapCircle(transform.position, _shieldRadius, 256);

        if (collidingObjects != null)
        {
            _waitBeforeDamage += Time.deltaTime;
            IDamageable enemy = collidingObjects.GetComponent<IDamageable>();

            if (enemy != null && _waitBeforeDamage > _timeBeforeDamage)
            {
                _waitBeforeDamage = 0;
                enemy.TakenDamge(_shieldDamage, _damagePerTick, true);
            }

            else if(enemy == null)
            {
                Destroy(collidingObjects.gameObject);
            }
        }
    }

    public void StopAbility()
    {
        transform.parent.GetComponent<PlayerAbilities>().abilityActive = false;
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _shieldRadius);
    }
}