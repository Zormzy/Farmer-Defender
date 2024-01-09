using System.Collections.Generic;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject _enemiesParent;
    [SerializeField] private GameObject _enemyNormalPrefab;
    [SerializeField] private GameObject _enemiesSpawnerPosition;
    private GameObject _enemyToActivate;
    private int _enemyMaxNumber;

    [Header("SpawnTimer")]
    public float _spawnTimer;
    public float _spawnTimerCounter;

    [Header("WavesVariables")]
    private int _waveMax;
    private int _actualWave;
    private int _enemiesNumberToSpawn;
    private Vector3 _enemiesSpawnedPosition;

    [Header("Lists")]
    public List<GameObject> activeEnemiesList;
    public List<GameObject> desactivatedEnemiesList;

    private void Awake()
    {
        EnemiesSpawnManagerInitialization();
    }

    private void Start()
    {
        EnemiesInstantiation();
    }

    private void Update()
    {
        SpawnTimer();
    }

    private void SpawnTimer()
    {
        if (_spawnTimerCounter < _spawnTimer)
            _spawnTimerCounter += Time.deltaTime;
        else if (_actualWave < _waveMax)
        {
            SpawnEnemies();
            _actualWave += 1;
            _enemiesNumberToSpawn += (5 * (_actualWave - 1));
            _spawnTimerCounter = 0f;
        }
    }

    private void SpawnEnemies()
    {
        if (desactivatedEnemiesList.Count > 0)
        {
            for (int i = 0; i < _enemiesNumberToSpawn; i++)
            {
                _enemyToActivate = desactivatedEnemiesList[desactivatedEnemiesList.Count - 1];
                desactivatedEnemiesList.RemoveAt(desactivatedEnemiesList.Count - 1);
                _enemyToActivate.transform.position = _enemiesSpawnedPosition;
                _enemyToActivate.SetActive(true);
                activeEnemiesList.Add(_enemyToActivate);
            }
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemy.SetActive(false);
        activeEnemiesList.Remove(enemy);
        desactivatedEnemiesList.Add(enemy);
    }

    private void EnemiesInstantiation()
    {
        for (int i = 0; i < _enemyMaxNumber; i++)
        {
            GameObject enemyNormal = Instantiate(_enemyNormalPrefab);
            enemyNormal.name = "EnemyNormal n°" + desactivatedEnemiesList.Count;
            enemyNormal.transform.SetParent(_enemiesParent.transform);
            enemyNormal.SetActive(false);
            desactivatedEnemiesList.Add(enemyNormal);
        }
    }

    private void EnemiesSpawnManagerInitialization()
    {
        activeEnemiesList = new List<GameObject>();
        desactivatedEnemiesList = new List<GameObject>();
        _enemyToActivate = null;
        _waveMax = 10;
        _actualWave = 1;
        _enemiesNumberToSpawn = 10;
        _enemyMaxNumber = _enemiesNumberToSpawn * (5 * _waveMax);
        _spawnTimer = 10f;
        _spawnTimerCounter = 0f;
        _enemiesSpawnedPosition = _enemiesSpawnerPosition.transform.position;
    }
}
