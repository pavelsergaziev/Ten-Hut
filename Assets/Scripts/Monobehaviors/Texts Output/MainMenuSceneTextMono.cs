using UnityEngine;
using TMPro;

using LanguagesAndTexts;

public class MainMenuSceneTextMono : TextsOutputMono_Base
{
    [SerializeField]
    private TextMeshProUGUI
    _mainMenu_StartGameLabelText, _mainMenu_HowToPlayLabelText,
    _mainMenu_HighScoreLabelText, _mainMenu_OptionsLabelText,
    _mainMenu_CreditsLabelText, _mainMenu_ExitGameLabelText,
    _howToPlayPanelText, _highScorePanelTitle, _creditsPanelText,
    _options_languageLabelText, _options_languageNameText, _options_voiceVolumeText, _options_fXVolumeText, _options_soundTestButtonText;

    [SerializeField]
    private TextMeshProUGUI[] _backToMenuButtons;

    protected override void SetTextValues(Texts texts, TMP_FontAsset font)
    {
        #region Main Menu

        SetTextAndFont(_mainMenu_StartGameLabelText, texts.StartGameButtonText, font);
        SetTextAndFont(_mainMenu_HowToPlayLabelText, texts.HowToPlayButtonText, font);
        SetTextAndFont(_mainMenu_HighScoreLabelText, texts.HighScoreButtonText, font);
        SetTextAndFont(_mainMenu_OptionsLabelText, texts.OptionsButtonText, font);
        SetTextAndFont(_mainMenu_CreditsLabelText, texts.CreditsButtonText, font);
        SetTextAndFont(_mainMenu_ExitGameLabelText, texts.ExitGameButtonText, font);

        #endregion

        #region back to menu buttons

        for (int i = 0; i < _backToMenuButtons.Length; i++)
        {
            SetTextAndFont(_backToMenuButtons[i], texts.BackToMenuButtonText, font);
        }

        #endregion

        #region HowToPlay panel
        string howToPlayTextControls = default;

#if UNITY_WEBGL
        howToPlayTextControls = texts.HowToPlayPanelText_controlsPC;
#endif
#if UNITY_STANDALONE
        howToPlayTextControls = texts.HowToPlayPanelText_controlsPC;
#endif
#if UNITY_ANDROID
        howToPlayTextControls = texts.HowToPlayPanelText_controlsMobile;
#endif

        SetTextAndFont(_howToPlayPanelText, string.Concat(texts.HowToPlayPanelText, "\n", howToPlayTextControls), font);

        #endregion

        SetTextAndFont(_highScorePanelTitle, texts.HighScorePanelText, font);
        SetTextAndFont(_creditsPanelText, texts.CreditsPanelText, font);

        #region options

        SetTextAndFont(_options_languageLabelText, texts.LanguageButtonText, font);
        SetTextAndFont(_options_languageNameText, texts.Language, font);
        SetTextAndFont(_options_voiceVolumeText, texts.VoiceVolumeOptionText, font);
        SetTextAndFont(_options_fXVolumeText, texts.FXVolumeOptionText, font);
        SetTextAndFont(_options_soundTestButtonText, texts.TestSoundButtonText, font);

        #endregion
    }

    private void SetTextAndFont(TextMeshProUGUI textField, string text, TMP_FontAsset font)
    {
        textField.text = text;
        textField.font = font;
    }
}