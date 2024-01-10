using System.Collections;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private Vector3 Direction = new(0,0,0);
    private GameObject Target;
    private Transform _transform;
    public float Speed = 40f;
    private int Damage = 0;

    private void Start()
    {
        _transform = transform;
    }
    private void OnEnable()
    {
        StartCoroutine(Disapear());
    }
    private void Update()
    {
        if(Target!= null)
            Direction = (Target.transform.position - _transform.position).normalized;
        _transform.position += Direction * Speed * Time.deltaTime;
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
    public void SetDirection(GameObject dir) => Target = dir;   
}
