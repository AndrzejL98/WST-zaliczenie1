using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIController : MonoBehaviour
{
    public static UIController instance;
    public Slider healthSlider;
    public Text healthText, coinText;
    public GameObject deathScreen;

    public Image fadeScreen;
    public float fadeSpeed;
    private bool fadeToBlack, fadeOutBlack;

    public string newGameScene, mainMenuScene;

    public GameObject pauseMenu, mapDisplay, bigMapText;

    public Image currentGun;
    public Text gunText;

    public Slider BossHealthBar;



    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        fadeOutBlack = true;
        fadeToBlack = false;

        currentGun.sprite = PlayerControler.instance.availableGuns[PlayerControler.instance.currentGun].gunUI;
        gunText.text = PlayerControler.instance.availableGuns[PlayerControler.instance.currentGun].weaponName;
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if(fadeScreen.color.a == 0)
            {
                fadeOutBlack = false;
            }
        }
        if (fadeToBlack)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1)
            {
                fadeToBlack = false;
            }
        }
    }
    public void StartFadeToBlack()
    {
        fadeToBlack = true;
        fadeOutBlack = false;
    }

    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(newGameScene);
        Destroy(PlayerControler.instance.gameObject);

    }
    public void ReturnMainMenu()
    {
        
        Time.timeScale = 1;
        SceneManager.LoadScene(mainMenuScene);
        Destroy(PlayerControler.instance.gameObject);
        
    }
    public void Resume()
    {
        LevelManager.instance.PauseUnpause();
    }
}
