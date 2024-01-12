using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]private Vector3 Direction = new(0,0,0);
    [SerializeField]private Vector3 FirstDirection = new(0, 0, 0);
    [SerializeField]private GameObject Target;
    private Transform _transform;
    private Transform targetTransform;
    public float Speed = 40f;
    private int Damage = 0;
    private bool aim = true;

    private void Start()
    {
        _transform = gameObject.transform;
    }
    private void OnEnable()
    {
        aim = true;
        StartCoroutine(Disapear());
    }
    private void Update()
    {
        
        if (Vector3.Distance(Target.transform.position , _transform.position) > 0.1f && aim && Target.activeSelf)
        {
            Direction = (Target.transform.position - _transform.position).normalized;
            Direction.y = 0;
            _transform.position += Direction * Speed * Time.deltaTime;
            transform.forward = Direction;
        }
        else
        {
            aim = false;
            _transform.position += FirstDirection * Speed * Time.deltaTime;
            transform.forward = FirstDirection;
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
            other.gameObject.GetComponent<EnemyManager>().EnemyTakeDamage(Damage);
            if(!_transform.CompareTag("Laser"))
            {
                StopAllCoroutines();
                BulletPulling.Instance.DestroyOne(gameObject);
            }
                
        }
    }

    public void SetDamage(int damage) => Damage = damage;
    public void SetDirection(GameObject dir)
    {
        Target = dir;
        if (Target != null)
            FirstDirection = (Target.transform.position - transform.position).normalized;
        FirstDirection.y = 0;
        targetTransform = Target.transform;
    }
}
