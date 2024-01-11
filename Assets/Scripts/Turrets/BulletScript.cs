using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]private Vector3 Direction = new(0,0,0);
    [SerializeField]private Vector3 FirstDirection = new(0, 0, 0);
    [SerializeField]private GameObject Target;
    private Transform _transform;
    public float Speed = 40f;
    private int Damage = 0;

    private void Start()
    {
        _transform = gameObject.transform;
    }
    private void OnEnable()
    {
        StartCoroutine(Disapear());
    }
    private void Update()
    {
        if(Target!= null && Vector3.Distance(Target.transform.position , _transform.position) < 0.1f)
        {
            Direction = (Target.transform.position - _transform.position).normalized;
            Direction.y = 0;
            _transform.position += Direction * Speed * Time.deltaTime;
        }
        else
        {
            _transform.position += FirstDirection * Speed * Time.deltaTime;
        }

    }

    private IEnumerator Disapear()
    {
        yield return new WaitForSeconds(5f);
        BulletPulling.Instance.DestroyOne(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other != null && other.gameObject.CompareTag("Enemy")) 
        {
            StopAllCoroutines();
            other.gameObject.GetComponent<EnemyManager>().EnemyTakeDamage(Damage);
            BulletPulling.Instance.DestroyOne(gameObject);
        }
    }

    public void SetDamage(int damage) => Damage = damage;
    public void SetDirection(GameObject dir)
    {
        Target = dir;
        if (Target != null)
            FirstDirection = (Target.transform.position - transform.position).normalized;
        FirstDirection.y = 0;
    }
}
