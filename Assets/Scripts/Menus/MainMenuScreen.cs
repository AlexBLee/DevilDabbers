using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScreen : MonoBehaviour
{
    public Button startButton;
    public Button optionsButton;
    public Button exitButton;

    private const string GAME_SCENE = "Main";

    // Use this for initialization
    void Start()
    {
        if (startButton != null) startButton.onClick.AddListener(StartButtonPressed);
        if (optionsButton != null) optionsButton.onClick.AddListener(OptionsButtonPressed);
        if (exitButton != null) exitButton.onClick.AddListener(ExitButtonPressed);
    }

    private void StartButtonPressed()
    {
        // Load game scene.
        SceneManager.LoadScene(GAME_SCENE);
    }

    private void OptionsButtonPressed()
    {
        MainMenuUIManager.instance.ToOptionsScreen();
    }

    private void ExitButtonPressed()
    {
        // Pop up confirmation dialogue.
    }
}