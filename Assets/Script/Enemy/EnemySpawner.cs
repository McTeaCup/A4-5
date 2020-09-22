using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Essentials")]
    [SerializeField] HUDManager playerHUD = default;
    [SerializeField] NewWaveManager waveManager = default;
    [SerializeField] GameObject nextWaveScreen = default;
    [SerializeField] Text enemiesRemaining = default;
    [SerializeField] Text _finalWave = default;
    Collider2D _playerCollider = default;
    [Tooltip("(Effect Managers)\nResponsible for visual effects")]
    [SerializeField] EffectManager effects = default;
    [Tooltip("(Spawn Points)\nKeeps track on the enemies path checkpoints")]
    [SerializeField] Transform[] spawnerPoint = default;
    [Tooltip("(Enemy Types)\nWhat enemies that can spawn")]
    [SerializeField] GameObject[] enemyTypes = default;

    [Header("Delays")]
    [Tooltip("Delay between enemies spawn")]
    [SerializeField] [Range(1, 5)] float spawnDelay = 3f;
    [SerializeField] [Range(1, 15)] float waveDelay = 10f;

    [Header("Total/Current Spawned Enemies")]
    int _totalEnemySpawnAmount = 0;
    float _nextTimeToSpawn;

    //Wave stuff
    int _currentWave;
    float _nextWaveWaitTime;
    NewWaveManager nextWaveManager = default;

    //Spawning Variabels
    [HideInInspector] public int enemisRemaining;
    [SerializeField]List<GameObject> enemySpawnList = default;
    bool spawnListMade = false;
    int currentSpawnableEnemies = default;
    int enemiesSpawnedThisWave;
    int spawnableEnemyIndex;
    float[] enemyTypeAmount = new float[5];

    void Awake()
    {
        _playerCollider = GameObject.Find("Player").GetComponent<CircleCollider2D>();
        _nextWaveWaitTime = waveDelay;
        SpawnEnemiesSetup();
        SpawnEnemies();
        enemisRemaining = _totalEnemySpawnAmount;
        nextWaveManager = nextWaveScreen.GetComponent<NewWaveManager>();
        nextWaveScreen.SetActive(false);
    }

    void Update()
    {
        WaveOnOff();
    }

    void WaveOnOff()
    {
        //Update HUD
        playerHUD.currentWave.text = $"Wave {_currentWave}";
        enemiesRemaining.text = $"Enemies remaining: {enemisRemaining}";
        
        //Spawn enemies if there are any left to spawn
        if (enemisRemaining != 0)
        {
            _playerCollider.enabled = true;
            SpawnEnemies();
        }

        if (enemisRemaining == 0) //if all enemies are dead
        {
            _nextWaveWaitTime -= Time.deltaTime;
            _playerCollider.enabled = false;
            nextWaveScreen.SetActive(true);
            nextWaveManager.NextWaveCountdown(_nextWaveWaitTime);

            //If the next wave's enemyspawnlist haven't been made
            if (!spawnListMade) 
            {
                SpawnEnemiesSetup();
                spawnListMade = true;
            }

            //If the wave delay is over
            if (_nextWaveWaitTime < 0)
            {
                nextWaveScreen.SetActive(false);
                enemisRemaining = _totalEnemySpawnAmount;
                _nextWaveWaitTime = waveDelay;
                spawnListMade = false;
            }
        }
    }

    void SpawnEnemiesSetup()
    {
        //Set the total amount of enemies on current wave
        enemySpawnList.Clear();
        enemiesSpawnedThisWave = 0;
        _currentWave++;
        _totalEnemySpawnAmount += _currentWave * 2;

        //Decide what type of enemies
        if (_currentWave >= 3)
        {
            currentSpawnableEnemies = 2;

            if (_currentWave >= 6)   //Spawn Even Stronger
            {
                currentSpawnableEnemies = 3;
                
                if (_currentWave >= 9)   //Spawn Even Stronger
                {
                    currentSpawnableEnemies = 4;
                    
                    if (_currentWave >= 12)   //Spawn Even Stronger
                    {
                        currentSpawnableEnemies = 5;
                    }
                }
            }
        }

        #region Spawn List Creator 
        for (int i = 0; i < _totalEnemySpawnAmount; i++)  //Spawn next enemy in list
        {
            spawnableEnemyIndex = Random.Range(0, currentSpawnableEnemies);
            enemySpawnList.Add(enemyTypes[spawnableEnemyIndex]);
        }

        spawnListMade = false;
        #endregion

        NextWaveHUDUpdate(); //Update "next wave screen"
    }

    void SpawnEnemies()
    {
        if (enemiesSpawnedThisWave != _totalEnemySpawnAmount)
        {
            for (int i = 0; i < enemySpawnList.Count; i++)
            {
                if (Time.time > _nextTimeToSpawn)
                {
                    _nextTimeToSpawn = Time.time + spawnDelay;
                    GameObject enemy = Instantiate(enemySpawnList[enemiesSpawnedThisWave],
                    spawnerPoint[spawnableEnemyIndex].GetChild(0).position,
                    enemySpawnList[enemiesSpawnedThisWave].transform.rotation);
                    enemiesSpawnedThisWave++;
                }
            }
        }
    }

    void NextWaveHUDUpdate()
    {
        
        //Find what enemies will spawn
        for (int i = 0; i < enemySpawnList.Count; i++)
        {
            //Sort the amount of each enemy
            switch (enemySpawnList[i].GetComponent<EMovement>().enemyProperties.movementType)
            {
                case MovementType.SideToSide:
                    {
                        enemyTypeAmount[0]++;
                        break;
                    }

                case MovementType.WShape:
                    {
                        enemyTypeAmount[1]++;
                        break;
                    }

                case MovementType.HourGlass:
                    {
                        enemyTypeAmount[2]++;
                        break;
                    }

                case MovementType.Circle:
                    {
                        enemyTypeAmount[3]++;
                        break;
                    }

                case MovementType.Swarm:
                    {
                        enemyTypeAmount[4]++;
                        break;
                    }
            }
        }

        //Sends all information to the wave manager
        waveManager.ApplyArtOnScreen(_currentWave, enemyTypeAmount[0],
            enemyTypeAmount[1], enemyTypeAmount[2], enemyTypeAmount[3],
            enemyTypeAmount[4]);
        _finalWave.text = _currentWave.ToString();
        effects.events[0].Invoke();
    }
}
