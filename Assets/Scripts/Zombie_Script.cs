using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie_Script : MonoBehaviour
{
    public Player_Move Script;
    
    void Update()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Script.Zombie_Speed + Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * Script.Zombie_Speed, Space.World);
    }
}
