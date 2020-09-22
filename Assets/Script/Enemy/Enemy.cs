using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    EnemyProperties _enemyProperties;
    EnemySpawner spawner;
    [SerializeField] GameObject deathEffect = default;
    [SerializeField]PointManger pointManager = default;
    float _pointMultiplier;

    [SerializeField] float _currentHealth = default;

    private void Awake()
    {
        _enemyProperties = transform.parent.GetComponent<EMovement>().enemyProperties;
        _currentHealth = _enemyProperties.health;
        spawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        pointManager = GameObject.Find("Game Manger").GetComponent<PointManger>();
    }

    public void TakenDamge(float damage, float pointMultiplier, bool addPointToMultiplier)
    {
        _currentHealth -= damage;
        _pointMultiplier = pointMultiplier;

        if (addPointToMultiplier)
        {
            pointManager.PlayerPointsUpdate(_enemyProperties.pointValue * (_pointMultiplier * 0.2f));
        }
    }

    private void Update()
    {
        if (_currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Instantiate(deathEffect, transform.position, Quaternion.Euler(90f, 0f, 0f));
        spawner.enemisRemaining--;
        pointManager.PlayerPointsUpdate(_enemyProperties.pointValue * _pointMultiplier);
        Destroy(transform.parent.gameObject); //Destroy object
    }
}
