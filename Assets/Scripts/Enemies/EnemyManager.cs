using System.Diagnostics;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Enemy_ScriptableObject _enemyScriptable;
    private EnemiesSpawnManager _enemiesSpawnManager;

    [Header("Varibales")]
    private int _enemyMaxHp;
    private int _enemyHp;
    public string _enemyName;
    public int _enemyDamage;

    private void Awake()
    {
        EnemyInitialization();
    }

    public void EnemyTakeDamage(int damage)
    {
        if (_enemyHp - damage <= 0)
        {
            _enemiesSpawnManager.OnEnemyDeath(gameObject);
            Economie.Instance.AddGolds(_enemyScriptable.gold);
        }
        else
            _enemyHp -= damage;
    }

    public void EnemyReset()
    {
        _enemyHp = _enemyMaxHp;
    }

    private void EnemyInitialization()
    {
        _enemiesSpawnManager = GameObject.Find("Enemies").GetComponent<EnemiesSpawnManager>();
        _enemyName = _enemyScriptable.enemyName;
        _enemyMaxHp = _enemyScriptable.enemyMaxHP;
        _enemyHp = _enemyMaxHp;
        _enemyDamage = _enemyScriptable.enemyDamage;
    }
}
