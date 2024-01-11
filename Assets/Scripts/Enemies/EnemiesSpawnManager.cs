using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesSpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] private GameObject _enemiesParent;
    [SerializeField] private GameObject _enemyNormalPrefab;
    [SerializeField] private GameObject _enemyTankPrefab;
    [SerializeField] private GameObject _enemyFastPrefab;
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

    public List<GameObject> activeTEnemiesList;
    public List<GameObject> desactivatedTEnemiesList ;

    public List<GameObject> activeFEnemiesList ;
    public List<GameObject> desactivatedFEnemiesList;

    [Header("VictoryCanvas")]
    [SerializeField] private TextMeshProUGUI _victoryStatusText;
    [SerializeField] private GameObject _victoryCanvas;

    public bool startSpawning = false;


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
        if (startSpawning)
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
            StartCoroutine(SpawnEnemies("Tank", _enemiesNumberToSpawn));
            StartCoroutine(SpawnEnemies("Normal", _enemiesNumberToSpawn));
            StartCoroutine(SpawnEnemies("Fast", _enemiesNumberToSpawn));
            _actualWave += 1;
            _actualWaveTxt.text = "Wave n° : " + _actualWave;
            _enemiesNumberToSpawn += (5 * (_actualWave - 1));
            _spawnTimerCounter = 0f;
        }
    }

    private IEnumerator SpawnEnemies(string Name, int numberOfSpawn)
    {
        if (desactivatedEnemiesList.Count > 0)
        {
            for (int i = 0; i < numberOfSpawn; i++)
            {
                _enemyToActivate = FoundEnemyByType(Name);
                FoundEnemyListDesactive(_enemyToActivate).Remove(_enemyToActivate);
                _enemyToActivate.transform.position = _enemiesSpawnedPosition;
                _enemyToActivate.SetActive(true);
                FoundEnemyListAlive(_enemyToActivate).Add(_enemyToActivate);
                _enemiesAliveNumber += 1;
                _enemiesAliveNumberTxt.text = "Enemies alive n° : \n" + _enemiesAliveNumber;
                yield return null;
            }
        }
    }

    private GameObject FoundEnemyByType(string Name)
    {
        switch (Name)
        {
            case "Normal": return desactivatedEnemiesList[desactivatedEnemiesList.Count - 1];
            case "Tank": return desactivatedTEnemiesList[desactivatedEnemiesList.Count - 1];
            case "Fast": return desactivatedFEnemiesList[desactivatedEnemiesList.Count - 1];
        }
        return null;
    }

    private List<GameObject> FoundEnemyListAlive(GameObject enemy)
    {
        string name = enemy.GetComponent<EnemyMovement>().GetName();

        switch (name) 
        {
            case "Normal": return activeEnemiesList;
            case "Tank": return activeTEnemiesList;
            case "Fast": return activeFEnemiesList;
        }
        return null;

    }
    private List<GameObject> FoundEnemyListDesactive(GameObject enemy)
    {
        string name = enemy.GetComponent<EnemyMovement>().GetName();

        switch (name)
        {
            case "Normal":
                return desactivatedEnemiesList;
            case "Tank": return desactivatedTEnemiesList;
            case "Fast": return desactivatedFEnemiesList;
        }
        return null;

    }

    public void OnEnemyDeath(GameObject enemy)
    {
        enemy.SetActive(false);
        _enemiesAliveNumber -= 1;
        _enemiesAliveNumberTxt.text = "Enemies alive n° : \n" + _enemiesAliveNumber;
        FoundEnemyListAlive(enemy).Remove(enemy);
        FoundEnemyListDesactive(enemy).Add(enemy);
    }

    private void EnemiesInstantiation()
    {
        EnnemiesSpawn(_enemyNormalPrefab, desactivatedEnemiesList, "EnemyNormal n°", _enemyMaxNumber);
        EnnemiesSpawn(_enemyTankPrefab, desactivatedTEnemiesList, "EnemyTank n°", _enemyMaxNumber);
        EnnemiesSpawn(_enemyFastPrefab, desactivatedFEnemiesList, "EnemyFast n°", _enemyMaxNumber);

    }

    private void EnnemiesSpawn(GameObject enemyPrefab, List<GameObject> stock, string Name, int number)
    {
        for (int i = 0; i < number; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab);
            enemy.name = Name + stock.Count;
            enemy.transform.SetParent(_enemiesParent.transform);
            enemy.SetActive(false);
            stock.Add(enemy);
        }
    }

    private void EnemiesSpawnManagerInitialization()
    {
        activeEnemiesList = new List<GameObject>();
        desactivatedEnemiesList = new List<GameObject>();
        activeTEnemiesList = new List<GameObject>();
        desactivatedTEnemiesList = new List<GameObject>();
        activeFEnemiesList = new List<GameObject>();
        desactivatedFEnemiesList = new List<GameObject>();
        _enemyToActivate = null;
        //_victoryStatusText = new();
        //_victoryCanvas = new();
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
