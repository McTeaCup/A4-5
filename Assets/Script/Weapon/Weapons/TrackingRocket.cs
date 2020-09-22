using UnityEngine;

public class TrackingRocket : MonoBehaviour, IBullet
{
    [SerializeField] GameObject _hitmark = default;
    Collider2D _target = null;
    IDamageable _targetIDamageable = default;
    [SerializeField] WeaponProperties _bulletProperties = default;
    [SerializeField] LayerMask _detection = default;
    float _acc = 0;

    private void Awake()
    {
        UponSpawn();
    }

    private void Update()
    {
        WhileAlive();
    }

    void OnDrawGizmos() //Draw gizmo, bullet detection area
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, _bulletProperties.rocketRadius);
    }

    public void UponDamge() //Sends information to score manager
    {
        _targetIDamageable.TakenDamge(_bulletProperties.damage, _bulletProperties.multiplierPoints, false);
        Destroy(gameObject);
    }

    public void UponSpawn() //Fetches enemy properties
    {
        _bulletProperties = GetComponent<ProjectileObject>().bulletProperties;
    }

    public void WhileAlive()
    {
        //If bomb has no target find one that contains the interface "IDamageable" and aproch it
        if (_target != null && _target.transform.GetComponent<IDamageable>() != null)
        {
            _targetIDamageable = _target.transform.GetComponent<IDamageable>();
            _acc += _bulletProperties.acceleration;
            transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, _acc * Time.deltaTime);
            transform.GetComponent<Rigidbody2D>().simulated = false;

            //If target doesn't have a hitmark, add one
            if (_target.transform.childCount == 0)
            {
                GameObject _hitmarkClone = Instantiate(_hitmark, _target.transform);
                _hitmarkClone.transform.position = _target.transform.position;
                _bulletProperties.survivaltime = 10f;
            }

            //If bullet is within 0.1 unites of target, deal damage
            if (Vector2.Distance(transform.position, _target.transform.position) < 0.1f)
            {
                Destroy(_target.transform.GetChild(0).gameObject);
                UponDamge();
            }
        }

        //If no target, find one within detection area
        else
        {
            _target = Physics2D.OverlapCircle(transform.position, _bulletProperties.rocketRadius, _detection);
            Destroy(gameObject, _bulletProperties.survivaltime * 0.5f);
            transform.GetComponent<Rigidbody2D>().simulated = true;
        }
    }
}