using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject _enemiesParent;
    [SerializeField] private GameObject _enemyNormalPrefab;
    [SerializeField] private GameObject _enemiesSpawnerPosition;
    [SerializeField] private TextMeshProUGUI _actualWaveTxt;
    [SerializeField] private TextMeshProUGUI _enemiesAliveNumberTxt;
    private GameObject _enemyToActivate;
    private int _enemyMaxNumber;
    private int _enemiesAliveNumber;

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

    [Header("VictoryCanvas")]
    [SerializeField] private TextMeshProUGUI _victoryStatusText;
    [SerializeField] private GameObject _victoryCanvas;

    private void Awake()
    {
        EnemiesSpawnManagerInitialization();
    }

    private void Start()
    {
        EnemiesInstantiation();
        _actualWaveTxt.text = "Wave n° : " + _actualWave;
        _enemiesAliveNumberTxt.text = "Enemies alive n° : \n" + _enemiesAliveNumber;
    }

    private void Update()
    {
        SpawnTimer();

        if (_actualWave == _waveMax)
            if (_enemiesAliveNumber <= 0)
            {
                _victoryStatusText.text = "Success";
                _victoryCanvas.SetActive(true);
            }
    }

    private void SpawnTimer()
    {
        if (_spawnTimerCounter < _spawnTimer)
            _spawnTimerCounter += Time.deltaTime;
        else if (_actualWave < _waveMax)
        {
            SpawnEnemies();
            _actualWave += 1;
            _actualWaveTxt.text = "Wave n° : " + _actualWave;
            _enemiesNumberToSpawn += (5 * (_actualWave - 1));
            _spawnTimerCounter = 0f;
        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemiesNumberToSpawn; i++)
        {
            _enemyToActivate = desactivatedEnemiesList[desactivatedEnemiesList.Count - 1];
            desactivatedEnemiesList.RemoveAt(desactivatedEnemiesList.Count - 1);
            _enemyToActivate.transform.position = _enemiesSpawnedPosition;
            _enemyToActivate.SetActive(true);
            _enemiesAliveNumber += 1;
            _enemiesAliveNumberTxt.text = "Enemies alive n° : \n" + _enemiesAliveNumber;
            activeEnemiesList.Add(_enemyToActivate);
        }
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemy.SetActive(false);
        _enemiesAliveNumber -= 1;
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
        _enemiesAliveNumber = 0;
        _enemiesNumberToSpawn = 10;
        _enemyMaxNumber = _enemiesNumberToSpawn * (5 * _waveMax);
        _spawnTimer = 10f;
        _spawnTimerCounter = 0f;
        _enemiesSpawnedPosition = _enemiesSpawnerPosition.transform.position;
    }
}
