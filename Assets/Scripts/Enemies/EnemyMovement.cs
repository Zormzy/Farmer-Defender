using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Enemy_ScriptableObject _enemyScriptable;
    private EnemiesSpawnManager _enemiesSpawnManager;
    private PlayerManager _playerManager;
    private GameObject _objectiveGO;
    private NavMeshAgent _enemyNavMeshAgent;

    [Header("Varibales")]
    private float _enemySpeed;
    private int _enemyDamage;
    private Vector3 _enemyDestination;

    private void Awake()
    {
        EnemyMovementInitialization();
    }

    private void Start()
    {
        _enemyNavMeshAgent.SetDestination(_enemyDestination);
    }

    private void Update()
    {
        if (_enemyNavMeshAgent.destination != _enemyDestination)
            _enemyNavMeshAgent.SetDestination(_enemyDestination);
    }

    private void EnemyMovementInitialization()
    {
        _enemiesSpawnManager = GameObject.Find("Enemies").GetComponent<EnemiesSpawnManager>();
        _playerManager = GameObject.Find("Level").GetComponent<PlayerManager>();
        _enemyNavMeshAgent = GetComponent<NavMeshAgent>();
        _enemySpeed = _enemyScriptable.enemySpeed;
        _enemyNavMeshAgent.speed = _enemySpeed;
        _enemyDamage = _enemyScriptable.enemyDamage;
        _objectiveGO = GameObject.FindGameObjectWithTag("Objective");
        _enemyDestination = _objectiveGO.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Objective"))
        {
            _playerManager.OnPlayerTakeDamage(_enemyDamage);
            _enemiesSpawnManager.OnEnemyDeath(gameObject);
        }
    }

    public string GetName() => _enemyScriptable.enemyName;
}
