using System.Collections;
using UnityEngine;

public class CAM2 : MonoBehaviour
{
    public Transform player;
    public bool play;
    public bool menu;
    [SerializeField] Animator Anim;
    [SerializeField] Animator Anim1;
    public Player_Move Move_Script;
    public Canvas Script;
    public float rotationSpeed;
    public AudioSource Collect;
    public AudioSource Huh;
    public AudioSource Zombie_Sound;
    private void Start()
    {
        play = false;
        menu = true;
    }
    void Update()
    {
        if (menu)
        {
            NC();

        }
        
            if (Script.TAPP && !play)
            {
                menu = false;
                play = true;
                Script.Menu.SetActive(false);
                Script.Pause_Button.SetActive(true);
                Script.Play.SetActive(true);
                Change();
                
        }
        
    }

    void NC()
    {
        Vector3 targetPosition = new Vector3(player.position.x + (-0.64f), player.position.y + (-1.6f), player.position.z + (-1.2f));
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);

        Quaternion targetRotation = Quaternion.Euler(player.rotation.eulerAngles.x + (-5f), player.rotation.eulerAngles.y + (-154f), player.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void Change()
    {
        Move_Script.Player_Speed = 2f; // LOOKBACK SPEED
        Move_Script.Zombie_Speed = 10f; // LOOKBACK SPEED
        Zombie_Sound.Play();
        Huh.Play();
        if (Script.BOY.activeSelf)
        {
            Anim.SetInteger("LB", 1);
        }
        else 
        {
            Anim1.SetInteger("LB", 1);
        }

        Invoke("CameraChange", 1.8f);
    }



    void CameraChange()
    {
        Vector3 newPosition = new Vector3(0.0f, 0f, player.position.z);
        transform.position = newPosition;

        Quaternion targetRotation = Quaternion.identity;
        StartCoroutine(SmoothRotateCoroutine(targetRotation, rotationSpeed));
    }

    IEnumerator SmoothRotateCoroutine(Quaternion targetRotation, float rotationSpeed)
    {
        float elapsedTime = 0f;
        Quaternion startingRotation = transform.rotation;

        while (elapsedTime < 1f)
        {
            transform.rotation = Quaternion.Slerp(startingRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * rotationSpeed;
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
