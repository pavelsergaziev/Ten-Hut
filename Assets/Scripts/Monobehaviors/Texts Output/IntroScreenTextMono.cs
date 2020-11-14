using UnityEngine;
using TMPro;
using LanguagesAndTexts;

public class IntroScreenTextMono : TextsOutputMono_Base
{
    [SerializeField]
    private TextMeshProUGUI _title, _secondaryTitle;

    protected override void SetTextValues(Texts texts, TMP_FontAsset font)
    {
        _title.text = texts.Title;
        _title.font = font;

        _secondaryTitle.text = texts.TitleSecondary;
        _secondaryTitle.font = font;
    }
}
