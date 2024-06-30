using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road_Spawner : MonoBehaviour
{
    public static Road_Spawner instance;
    [SerializeField] GameObject City;
    [SerializeField] Transform Spawn_Location;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Spawn_City()
    {
        Instantiate(City, Spawn_Location.position, Quaternion.identity);
        Spawn_Location.position = new Vector3(Spawn_Location.transform.position.x, Spawn_Location.transform.position.y, Spawn_Location.transform.position.z + 150.3f);
    }
    private void OnDestroy()
    {
        Destroy(instance);
    }
}
