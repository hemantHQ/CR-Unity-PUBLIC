using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Canvas : MonoBehaviour
{
    public GameObject Menu, Play, Pause_Button,Pause, Dead,L_Button,R_Button,ME,UPGRADE, Me_pro,panel,Upgarde_c,Settings_c,cancel_1,cancel_2;
    public GameObject BOY, GIRL;
    public Player_Move Script;
    public CAM2 Script1;
    public bool TAPP;
    [SerializeField] TextMeshProUGUI Coin_sys,Score_sys;
    private int score = 0;
    public AudioSource Background_Music;
    public TextMeshProUGUI you_scored,you_coin;
    bool Score_Dead;

    void Start()
    {
        PlayerPrefs.DeleteAll();
        Play.SetActive(false);
        Menu.SetActive(true);
        Pause.SetActive(false);
        Pause_Button.SetActive(false);
        Dead.SetActive(false);
        TAPP = false;
        Me_pro.SetActive(false);
        
    }
    public void Settings()
    {
        Menu.SetActive(false);
        panel.SetActive(true);
        Settings_c.SetActive(true);
    }
    public void TaptoPlay()
    {
        TAPP = true;
    }
    public void Pause_Press()
    {
        Pause_Button.SetActive(false);
        panel.SetActive(true);
        Script.Zombie_Speed = 0f;
        Script.Player_Speed = 0f;
        Time.timeScale = 0f;
        Pause.SetActive(true);
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        Script1.play = false;//fix need help
    }

    public void Continue()
    {
        Pause.SetActive(false);
        panel.SetActive(false);
        Time.timeScale = 1f;
        Pause_Button.SetActive(true);
        Script.Zombie_Speed = 0.015f;
        Script.Player_Speed = 0.015f;
    }
    public void Left_button()
    {
        BOY.SetActive(true);
        L_Button.SetActive(false);
        R_Button.SetActive(true);
        GIRL.SetActive(false);
    }
    public void Right_button()
    {
        BOY.SetActive(false);
        R_Button.SetActive(false);
        L_Button.SetActive(true);
        GIRL.SetActive(true);
    }
    public void  Upgrade()
    {
        Menu.SetActive(false);
        panel.SetActive(true);
        Upgarde_c.SetActive(true);
    }

    public void ME_Back()
    {
        Settings_c.SetActive(false);    
        Me_pro.SetActive(false);
        Upgarde_c.SetActive(false);
        panel.SetActive(false);
        Menu.SetActive(true);

    }
    public void Sound_B()
    {
        if (cancel_1.activeSelf)
        {
            Script.Dead_Sound.Play();
            Script1.Collect.Play();
            Script1.Huh.Play();
            Script1.Zombie_Sound.Play();
            cancel_1.SetActive(false);
        }
        else
        {
            Script.Dead_Sound.Stop();
            Script1.Collect.Stop();
            Script1.Huh.Stop();
            Script1.Zombie_Sound.Stop();
            cancel_1.SetActive(true);
        }
    }
    public void Music_B()
    {
        if (cancel_2.activeSelf)
        {
            Background_Music.Play();
            cancel_2.SetActive(false);
        }
        else
        {
            Background_Music.Stop();
            cancel_2.SetActive(true);
        }
    }
    
    public void  Me()
    {
        Menu.SetActive(false);
        Me_pro.SetActive(true);
        if(BOY.activeSelf)
        {
            L_Button.SetActive(false);
            R_Button.SetActive(true);
        }
        else if(GIRL.activeSelf)
        {
            L_Button.SetActive(true);
            R_Button.SetActive(false);
        }
    }

    public void Github()
    {
        string gitURL = "https://github.com/hemantHQ";
        Application.OpenURL(gitURL);
    }
    public void Insta()
    {
        string instaURL = "https://www.instagram.com/hemantk7020";
        Application.OpenURL(instaURL);
    }
    public void Telegram()
    {
        string teleURL = "https://t.me/HARAME420";
        Application.OpenURL(teleURL);
    } 
    public void Privacy_policy()
    {
        string teleURL = "https://t.me/HARAME420";
        Application.OpenURL(teleURL);
    }

    private void Update()
    {
        Score_Dead= Script.Dead;
        you_scored.text = Score_sys.text.ToString();
        you_coin.text= Coin_sys.text.ToString() ;
        if (Pause_Button.activeSelf)
        {
            Coin_sys.text = PlayerPrefs.GetInt("Counter", 0).ToString("0");
            Invoke("Score_Timer", 1.9f);
        }
    }

    void Score_Timer()
    {
        if (!Score_Dead) { 
        score++;
        Score_sys.text = score.ToString();
        }
    }
}