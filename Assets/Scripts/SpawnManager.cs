using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    MapSpawner mapSpawner;
    // Start is called before the first frame update
    void Start()
    {
        mapSpawner = GetComponent<MapSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnerTriggerEntered()
    {
        mapSpawner.MoveMap();
    }
}
