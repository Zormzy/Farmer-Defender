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
    [SerializeField]private bool aim = true;
    private Vector2 PosA, PosB = Vector2.zero;

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
        PosA.Set(Target.transform.position.x, Target.transform.position.z);
        PosB.Set(_transform.position.x, _transform.position.z);

        if (Vector2.Distance(PosA, PosB) > 0.1f && aim && Target.activeSelf)
        {
            Direction = (Target.transform.position - _transform.position).normalized;
            Direction.y = 0;
            _transform.position += Direction * Speed * Time.deltaTime;
        }
        else
        {
            aim = false;
            _transform.position += FirstDirection * Speed * Time.deltaTime;
        }
        transform.forward = FirstDirection;

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
            if(other.gameObject == Target) { aim = false; }
            other.gameObject.GetComponent<EnemyManager>().EnemyTakeDamage(Damage);
            if(!_transform.CompareTag("Laser") || other.gameObject.CompareTag("Meteor"))
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
