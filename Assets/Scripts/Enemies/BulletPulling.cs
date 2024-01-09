using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPulling : MonoBehaviour
{
    public static BulletPulling Instance;

    [SerializeField] private int numberOfBulletToInstanciate = 100;
    public GameObject bulletPrefab;

    private List<GameObject> bulletsActive = new();
    private Queue<GameObject> bulletsNotActive = new();
    // Start is called before the first frame update
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
