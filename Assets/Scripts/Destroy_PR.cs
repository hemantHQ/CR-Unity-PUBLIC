using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_PR : MonoBehaviour
{
    GameObject Player;
    public Obstacles Script;
    void Start()
    {
        Player = GameObject.Find("Player").gameObject;
    }

    void Update()
    {
        if (Player.transform.position.z > transform.position.z + 125f)
        {
            Road_Spawner.instance.Spawn_City();
            Destroy(this.gameObject);
        }
    
    }
}
