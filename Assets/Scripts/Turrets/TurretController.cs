using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Turrets_Info info;
    [SerializeField]List<GameObject> MyEnnemies = new List<GameObject>();
    private Transform _transform;
    private GameObject enemyToAttack;
    [SerializeField]private float attackWaitTime = 0.5f;
    private float attackTimer;
    public GameObject bulletPrefab;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        attackWaitTime = 1 / info.fireRate;
        attackTimer = Time.time + attackWaitTime;

        gameObject.GetComponent<SphereCollider>().radius = info.Range;
    }

    // Update is called once per frame
    void Update()
    {
        enemyToAttack =  FindNearestEnnemy();
        if (enemyToAttack != null && Time.time > attackTimer) 
        {
            attackTimer = Time.time + attackWaitTime;
            GameObject go = BulletPulling.Instance.GetNew();
            go.transform.position = _transform.position;
            go.GetComponent<BulletScript>().SetDamage(info.damage);
            go.GetComponent<BulletScript>().SetDirection(enemyToAttack);
        }
    }

    private GameObject FindNearestEnnemy()
    {
        GameObject nearestEnemy = null;
        float distance = Mathf.Infinity;
        for (int i = 0; i < MyEnnemies.Count; i++)
        {
            if (!MyEnnemies[i].activeSelf)
            {
                MyEnnemies.Remove(MyEnnemies[i]);
                i--;
            }
        }
        for (int i = 0; i < MyEnnemies.Count; i++) 
        {
            if(Vector3.Distance(_transform.position, MyEnnemies[i].transform.position) < distance)
            {
                distance = Vector3.Distance(_transform.position, MyEnnemies[i].transform.position);
                nearestEnemy = MyEnnemies[i];
            }
        }
        return nearestEnemy;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Enemy"))
        {
            if (!MyEnnemies.Contains(collision.gameObject))
                MyEnnemies.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.gameObject.CompareTag("Enemy"))
        {
            if (MyEnnemies.Contains(other.gameObject))
                MyEnnemies.Remove(other.gameObject);
        }
    }
}
