using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public bool played;
    public GameObject[] Levels;

    private void Update()
    {
       
        CAM2 sc = FindObjectOfType<CAM2>();
        played = sc.play;
        if (played)
        {
            Spawn_Road();
        }
    }

    void Spawn_Road()
    {
        int random_value = Random.Range(0, Levels.Length);
        Levels[random_value].gameObject.SetActive(true);
    }
}
