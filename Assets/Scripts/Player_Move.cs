using UnityEngine;
using System.Collections;
using Unity.VisualScripting;


public class Player_Move : MonoBehaviour
{
    [SerializeField] Transform center, left, right;
    [SerializeField] Rigidbody rb;
    [SerializeField] Animator Anim;
    [SerializeField] Animator Anim1;
    public float side_speed, jump, Player_Speed, Zombie_Speed;
    int current_position = 0;
    public bool groundcontact, Tapped, Dead;
    public CAM2 Script;
    public Canvas Script1;

    private Vector2 touchStartPosition;
    private Vector2 touchEndPosition;
    private bool isSwiping = false;
    private float minSwipeDistance = 50f;
    public AudioSource Dead_Sound;


    private void Start()
    {
        Dead = false;
        Application.targetFrameRate = 60;
        groundcontact = true;
        current_position = 0;
        Zombie_Speed = 3.5f;
        Player_Speed = 3.5f;   // START RUNNING SPEED 
        Tapped = false;
        rb = GetComponent<Rigidbody>();
    }


    void Runninf()
    {
        if (!Dead)
        {
            Player_Speed = 12.5f;     // AFTER TAP SPEED
            if (Script1.BOY.activeSelf)
            {
                Anim.SetInteger("Running", 1);
            }
            else if (Script1.GIRL.activeSelf)
            {
                Anim1.SetInteger("Running", 1);
            }
            Zombie_Speed = Player_Speed;
            Tapped = true;
        }
    }

    void Update()
    {
        // transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Player_Speed + Time.deltaTime);
        //-1 is left , 0 center , 1 right.
        transform.Translate(Vector3.forward * Time.deltaTime * Player_Speed, Space.World);
        bool Runing = Script.play;
        if (Runing)
        {
            Invoke("Runninf", 1.8f);
            if (Tapped)
            {
                DetectSwipe();
                if (current_position == 0)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        current_position = -1;
                    }
                    else if (Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        current_position = 1;
                    }
                }
                else if (current_position == -1)
                {
                    if (Input.GetKeyDown(KeyCode.RightArrow))
                    { current_position = 0; }
                }
                else if (current_position == 1)
                {
                    if (Input.GetKeyDown(KeyCode.LeftArrow))
                    { current_position = 0; }
                }

                if (current_position == 0)
                {
                    if (Vector3.Distance(transform.position, new Vector3(center.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                    {
                        Vector3 dir = new Vector3(center.position.x, transform.position.y, transform.position.z) - transform.position;
                        transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                    }
                }
                else if (current_position == -1)
                {
                    if (Vector3.Distance(transform.position, new Vector3(left.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                    {
                        Vector3 dir = new Vector3(left.position.x, transform.position.y, transform.position.z) - transform.position;
                        transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                    }
                }
                else if (current_position == 1)
                {
                    if (Vector3.Distance(transform.position, new Vector3(right.position.x, transform.position.y, transform.position.z)) >= 0.1f)
                    {
                        Vector3 dir = new Vector3(right.position.x, transform.position.y, transform.position.z) - transform.position;
                        transform.Translate(dir.normalized * side_speed * Time.deltaTime, Space.World);
                    }
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (groundcontact)
                    {
                        Jump();
                    }
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (groundcontact)
                    {
                        Slide();
                    }
                }
            }
        }
    }
    private void DetectSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPosition = touch.position;
                isSwiping = true;
            }
            else if (touch.phase == TouchPhase.Moved && isSwiping)
            {
                touchEndPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended && isSwiping)
            {
                float swipeDistanceX = touchEndPosition.x - touchStartPosition.x;
                float swipeDistanceY = touchEndPosition.y - touchStartPosition.y;

                if (Mathf.Abs(swipeDistanceX) > minSwipeDistance || Mathf.Abs(swipeDistanceY) > minSwipeDistance)
                {
                    if (Mathf.Abs(swipeDistanceX) > Mathf.Abs(swipeDistanceY))
                    {
                        if (swipeDistanceX > 0)
                        {
                            // Swipe Right
                            if (current_position == -1)
                            { current_position = 0; }
                            else if (current_position == 0)
                            { current_position = 1; }
                        }
                        else
                        {
                            // Swipe Left
                            if (current_position == 1)
                            { current_position = 0; }
                            else if (current_position == 0)
                            { current_position = -1; }
                        }
                    }
                    else
                    {
                        if (swipeDistanceY > 0)
                        {
                            // Swipe Up
                            Jump();
                        }
                        else
                        {
                            // Swipe Down
                            Slide();
                        }
                    }
                }

                isSwiping = false;
            }
        }
    }
    void Jump()
    {
        // int Jump_Random = Random.Range(1, 3);
        groundcontact = false;
        rb.velocity = Vector3.up * jump;
        if (Script1.BOY.activeSelf)
        {
            Anim.SetInteger("JUMP_1", 1);
        }
        else
        {
            Anim1.SetInteger("JUMP_1", 1);
        }
        Invoke("jumpanimation", 0.55f);
    }
    void Slide()
    {

        if (Script1.BOY.activeSelf)
        {
            Anim.SetInteger("Slide", 1);
        }
        else
        {
            Anim1.SetInteger("Slide", 1);
        }
        Invoke("jumpanimation", 0.2f);
    }
    void jumpanimation()
    {
        if (Script1.BOY.activeSelf)
        {
            Anim.SetInteger("JUMP_1", 0);
            Anim.SetInteger("Slide", 0);
        }
        else
        {
            Anim1.SetInteger("JUMP_1", 0);
            Anim1.SetInteger("Slide", 0);
        }
    }
    void afterdead()
    {
        Time.timeScale = 0f;
        Script1.panel.SetActive(true);
        Script1.Dead.SetActive(true);
        Script1.Pause_Button.SetActive(false);
        Script1.Pause.SetActive(false);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ROAD"))
        {
            groundcontact = true;
        }
        if (collision.gameObject.CompareTag("Dead"))
        {
            Dead_Sound.Play();
            Dead = true;
            Script1.Play.SetActive(false);
            Zombie_Speed = 0f;
            Player_Speed = 0f;
            if (Script1.BOY.activeSelf)
            {
                Anim.SetInteger("Death", 1);
            }
            else
            {
                Anim1.SetInteger("Death", 1);
            }
            Invoke("afterdead", 1.4f);

        }
    }
}
