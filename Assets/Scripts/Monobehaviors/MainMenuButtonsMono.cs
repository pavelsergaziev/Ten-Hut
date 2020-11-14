using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButtonsMono : MonoBehaviour
{
    private UIButtonEffectsController _uIButtonEffectsController;

    private void Start()
    {
        _uIButtonEffectsController = Main.Instance.UIButtonEffectsController;
    }

    private void ButtonWasPressed(MenuButtons button)
    {
        _uIButtonEffectsController.ButtonEffectRequested(button);
    }

    public void StartGameButtonPressed()
    {
        ButtonWasPressed(MenuButtons.StartGame);
    }

    public void HowToPlayButtonPressed()
    {
        ButtonWasPressed(MenuButtons.HowToPlay);
    }
    public void OptionsButtonWasPressed()
    {
        ButtonWasPressed(MenuButtons.Options);
    }
    public void HighScoreButtonPressed()
    {
        ButtonWasPressed(MenuButtons.HighScore);
    }
    public void CreditsButtonPressed()
    {
        ButtonWasPressed(MenuButtons.Credits);
    }
    public void ExitGameButtonPressed()
    {
        ButtonWasPressed(MenuButtons.ExitGame);
    }
    public void ReturnToMainMenuButtonPressed()
    {
        ButtonWasPressed(MenuButtons.ReturnToMainMenu);
    }
}

