using UnityEngine;

public class Coin : MonoBehaviour
{
    bool PLAYED;
    private AudioSource Coin_Collect;
    
    private void Start()
    {
        //PlayerPrefs.DeleteAll();
    }
    void Update()
    {
        CAM2 sc = FindObjectOfType<CAM2>();
        PLAYED = sc.play;
        Coin_Collect = sc.Collect;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (PLAYED)
        {
            if (other.gameObject.tag == "Player")
            {
                Destroy(this.gameObject);
                Coin_Collect.Play();
                PlayerPrefs.SetInt("Counter", PlayerPrefs.GetInt("Counter", 0) + 1);

            }
        }
    }
}
