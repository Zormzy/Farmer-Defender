using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public Turrets_Info info;
    [SerializeField] List<GameObject> MyEnnemies = new List<GameObject>();
    private Transform _transform;
    private Transform _turretTransform;
    private GameObject enemyToAttack;
    [SerializeField] private float attackWaitTime = 0.5f;
    private float attackTimer;
    public GameObject bulletPrefab;
    public FireType fireType;

    public enum FireType
    {
        Bullet, Laser
    }
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        attackWaitTime = 1 / info.fireRate;
        attackTimer = Time.time + attackWaitTime;

        _turretTransform = transform.parent;
        gameObject.GetComponent<SphereCollider>().radius = info.Range;
    }

    // Update is called once per frame
    void Update()
    {
        enemyToAttack = FindNearestEnnemy();

        if (enemyToAttack != null)
            _turretTransform.LookAt(enemyToAttack.transform);

        if (enemyToAttack != null && Time.time > attackTimer)
        {
            attackTimer = Time.time + attackWaitTime;
            GameObject go = GetRightBullet();
            go.transform.position = _transform.position + new Vector3(0, 1, 0);
            go.GetComponent<BulletScript>().SetDamage(info.damage);
            go.GetComponent<BulletScript>().SetDirection(enemyToAttack);
        }
    }

    public GameObject GetRightBullet()
    {
        GameObject go = null;
        switch (fireType)
        {
            case FireType.Bullet:
                go = BulletPulling.Instance.GetNew();
                break;
            case FireType.Laser:
                go = Instantiate(bulletPrefab);
                go.transform.LookAt(enemyToAttack.transform);
                break;
        }
        return go;
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
            if (Vector3.Distance(_transform.position, MyEnnemies[i].transform.position) < distance)
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
