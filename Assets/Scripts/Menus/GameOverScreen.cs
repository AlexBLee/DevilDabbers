using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public GameManager gm;
    public Canvas canvas;
    public Text text;
    public Button restartButton;
    public Button menuButton;

    private const string MAIN_MENU = "MainMenu";
    private const string GAME_SCENE = "Main";


    // Use this for initialization
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        if (canvas != null) canvas.GetComponent<Canvas>().gameObject.SetActive(false);
        if (restartButton != null) restartButton.onClick.AddListener(RestartButtonPressed);
        if (menuButton != null) menuButton.onClick.AddListener(MainMenuButtonPressed);
    }

    void Update()
    {
        
    }

    private void RestartButtonPressed()
    {
        // Load game scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    private void MainMenuButtonPressed()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void Time()
    {
        text.text = "You survived for " + gm.GetTime().ToString("F2") + " seconds!";
    }
}
