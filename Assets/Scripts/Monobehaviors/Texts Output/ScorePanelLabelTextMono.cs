using UnityEngine;
using TMPro;
using LanguagesAndTexts;

public class ScorePanelLabelTextMono : TextsOutputMono_Base
{
    [SerializeField]
    private TextMeshProUGUI _labelText;

    protected override void SetTextValues(Texts texts, TMP_FontAsset font)
    {
        _labelText.text = texts.ScorePanelText;
        _labelText.font = font;
    }
}
