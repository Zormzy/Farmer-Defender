using System.Collections.Generic;
using UnityEngine;

public class BulletPulling : MonoBehaviour
{
    [SerializeField] private GameObject _bulletsParent;
    public static BulletPulling Instance;
    [SerializeField] private int numberOfBulletToInstanciate = 100;
    public GameObject bulletPrefab;
    private List<GameObject> bulletsActive = new();
    private Queue<GameObject> bulletsNotActive = new();

    void Start()
    {
        if (Instance == null) { Instance = this; }

        Initialized();
    }

    private void Initialized()
    {
        for(int i = 0; i < numberOfBulletToInstanciate; i++) 
        {
            GameObject go = Instantiate<GameObject>(bulletPrefab);
            go.transform.parent = _bulletsParent.transform;
            go.SetActive(false);
            bulletsNotActive.Enqueue(go);
        }
    }

    public GameObject GetNew()
    {
        GameObject go = bulletsNotActive.Dequeue();
        go.SetActive(true);
        bulletsActive.Add(go);
        return go;
    }

    public void DestroyOne(GameObject go)
    {
        bulletsActive.Remove(go);
        go.SetActive(false);
        bulletsNotActive.Enqueue(go);
    }
}
