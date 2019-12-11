using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public int maxX;
    public int maxZ;
    public float newSpawnTime;
    public int cantPickUps;
    public GameObject pickUpGO;

    void Start()
    {
        for (int i = 0; i < cantPickUps; i++)
        {
            CreateCollectable();
        }
    }
    public GameObject CreateCollectable()
    {
        GameObject aux = Instantiate(pickUpGO);
        aux.transform.position = new Vector3(Random.Range(-maxX, maxX), 40f, Random.Range(-maxZ, maxZ));
        return aux;
    }
}