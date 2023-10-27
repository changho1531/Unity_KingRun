using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public List<GameObject> maps;

    public float offset = -450f;
    // Start is called before the first frame update
    void Start()
    {
        if (maps != null && maps.Count > 0)
        {
            maps = maps.OrderByDescending(r => r.transform.position.z).ToList();
        }
    }

    public void MoveMap()
    {
        GameObject moveMap = maps[0];
        maps.Remove(moveMap);
        float newZ = maps[maps.Count -1 ].transform.position.z + offset;
        moveMap.transform.position = new Vector3(0, 0, newZ);
        maps.Add(moveMap);

    }
}
