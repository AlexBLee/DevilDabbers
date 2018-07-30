using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScreen : MonoBehaviour
{
    public Button backButton;
    public Slider audioSlider;
    public ToggleGroup difficultyToggleGroup;

	// Use this for initialization
	void Start ()
    {
        if (backButton != null) backButton.onClick.AddListener(BackButtonPressed);
        // if (audioSlider != null) audioSlider.onValueChanged.AddListener( (val) => { AudioSliderChanged(val); });
        if (audioSlider != null) audioSlider.onValueChanged.AddListener(AudioSliderChanged);

    }

    private void BackButtonPressed()
    {
        MainMenuUIManager.instance.ToMainMenuScreen();
    }

    private void AudioSliderChanged(float val)
    {
        // Set a value in an options manager or audio manager class.
    }

    private void DifficultyToggled(bool easy)
    {
        // Set a value in an options manager or game manager class.
    }

}
