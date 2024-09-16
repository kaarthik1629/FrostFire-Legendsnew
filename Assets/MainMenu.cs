using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject Optionsmenu;
    [SerializeField] GameObject SoundPanel;
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject DeathMenu;
    [SerializeField] GameObject MainResume;
    private bool isPaused = false;
    public  bool playerdead = false;
    public static MainMenu instance;
    

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Update()
    {
        if (playerdead) return;

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (isPaused)
            {
                ResumeGame();

            }
            else
            {
                PauseGame();

            }

        }
            
    }

    public void playgame()
    {

        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
        playerdead = false;


    }
    public void PauseGame()
    {
        Optionsmenu.SetActive(true);
        SoundPanel.SetActive(false);
        buttons.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void ResumeGame()
    {
        Optionsmenu.SetActive(false);
        SoundPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused= false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }
    public void QuitToTitleScreen()
    {

       // Optionsmenu.SetActive(false);
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    public void PlayerDeath()
    {
        DeathMenu.SetActive(true);
        Time.timeScale = 0f;
        playerdead = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    //public void MainResumebutton()
    //{
    //    Optionsmenu.SetActive(false);
    //    SoundPanel.SetActive(false);
    //    Time.timeScale = 1f;
    //   // isPaused = false;


    //}
}
