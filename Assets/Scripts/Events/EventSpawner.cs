using System.Collections.Generic;
using UnityEngine;

public class EventSpawner : MonoBehaviour
{
    [Header("Meteor")]
    [SerializeField] private GameObject _meteorPrefab;
    [SerializeField] private GameObject _meteorParent;
    private Stack<GameObject> _meteorStack;

    [Header("Variables")]
    private int _meteorCount;
    public bool _gameStarted;
    public float _eventTimer;
    public float _eventTimerCounter;
    private GameObject _activeMeteor;

    private void Awake()
    {
        EventSpawnerInitialization();
    }

    private void Start()
    {
        MeteorInstantiation();
    }

    private void Update()
    {
        if (_gameStarted)
        {
            if (_eventTimerCounter < _eventTimer)
                _eventTimerCounter += Time.deltaTime;
            else
            {
                if (_meteorStack.Count > 0)
                {
                    _activeMeteor = _meteorStack.Pop();
                    _activeMeteor.SetActive(true);
                    _eventTimer = Random.Range(30f, 90f);
                    _eventTimerCounter = 0f;
                }
            }
        }
    }

    private void MeteorInstantiation()
    {
        for (int i = 0; i < _meteorCount; i++)
        {
            GameObject meteor = Instantiate(_meteorPrefab);
            meteor.transform.parent = _meteorParent.transform;
            meteor.name = "Meteor n°" + _meteorCount;
            meteor.SetActive(false);
            _meteorStack.Push(meteor);
        }
    }

    private void EventSpawnerInitialization()
    {
        _gameStarted = false;
        _meteorCount = 10;
        _eventTimer = Random.Range(30f, 90f);
        _eventTimerCounter = 0f;
        _meteorStack = new Stack<GameObject>();
    }
}
