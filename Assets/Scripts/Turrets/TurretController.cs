using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField]List<GameObject> MyEnnemies = new List<GameObject>();
    private Transform _transform;
    private GameObject enemyToAttack;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        enemyToAttack =  FindNearestEnnemy();
        if (enemyToAttack != null ) 
        {
            Debug.Log("attack");
        }
    }

    private GameObject FindNearestEnnemy()
    {
        GameObject nearestEnemy = null;
        float distance = 1000;
        for(int i = 0; i < MyEnnemies.Count; i++) 
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
