using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    // Easiest, laziest, and worst way to make a Singleton.
    public static MainMenuUIManager instance;

    public MainMenuScreen mainMenuScreen;
    public OptionsScreen optionsScreen;

    public RectTransform titleRT;

    private RectTransform currentScreen;

    private void Awake()
    {
        instance = this;
    }


    // Use this for initialization
    void Start()
    {
        LeanTween.alpha(mainMenuScreen.GetComponent<RectTransform>(), 0.0f, 0.0f);
        LeanTween.scale(titleRT, new Vector3(2f, 2f, 2f), 1.0f).setEaseInCubic().setOnComplete( () =>
        {
            LeanTween.scale(titleRT, new Vector3(1f, 1f, 1f), 1.0f).setEaseInCirc();
            LeanTween.moveY(titleRT, 100f, 1.0f);
            LeanTween.rotate(titleRT, new Vector3(1080f, 1080f, 1080f), 1.0f).setOnComplete(() => 
            {
                LeanTween.alpha(mainMenuScreen.GetComponent<RectTransform>(), 1.0f, 1.0f);
                currentScreen = mainMenuScreen.GetComponent<RectTransform>();
            });


        });


        // scaleTween.setEaseInBack
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToOptionsScreen()
    {
        ChangeScreen(optionsScreen.GetComponent<RectTransform>());
    }

    public void ToMainMenuScreen()
    {
        ChangeScreen(mainMenuScreen.GetComponent<RectTransform>());
    }

    private void ChangeScreen(RectTransform nextScreen)
    {
        if(nextScreen != currentScreen)
        {
            float xPos = nextScreen.rect.width;
            nextScreen.position = new Vector3(xPos*1.5f, nextScreen.position.y, nextScreen.position.z);
            // tween current screen out and deactivate
            LeanTween.moveX(currentScreen, xPos, 1.0f).setOnComplete(() => 
            {
                currentScreen.gameObject.SetActive(false);
                // activate and tween next screen in
                nextScreen.gameObject.SetActive(true);
                LeanTween.moveX(nextScreen, 0.0f, 1.0f).setOnComplete(() => { currentScreen = nextScreen; });
            });

            currentScreen = nextScreen;
        }
    }
}
