using UnityEngine;

public class MeteorController : MonoBehaviour
{
    [Header("Components")]
    private Transform _transform;
    [SerializeField] private GameObject _meteorFloor;

    [Header("Variables")]
    private float _speed;
    private Vector3 _target;
    private Vector3 _direction;
    private Vector3 _startPositon;

    private void Awake()
    {
        MateorControllerInitialization();
    }

    private void OnEnable()
    {
        _transform.position = _startPositon;
        _target = new Vector3(Random.Range(-50f, 66f), 2.5f, Random.Range(-42f, 41f));
        _direction = (_target - _transform.position).normalized;
    }

    private void Update()
    {
        if (_transform.position.y > _target.y)
            _transform.position += _speed * Time.deltaTime * _direction;
        else if (!_meteorFloor.activeSelf)
            _meteorFloor.SetActive(true);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision != null && collision.CompareTag("Turret") || 
            collision != null && collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }
    }

    private void MateorControllerInitialization()
    {
        _target = Vector3.zero; 
        _direction = Vector3.zero;
        _startPositon = new Vector3(0, 100f, 0);
        _transform = transform;
        _speed = 50f;
    }
}
