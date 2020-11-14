using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "LanguageSO", menuName = "GameScriptableObjectsAsset/LanguageSO")]
public class LanguageSO :  ScriptableObject
{
    [SerializeField]
    private TextAsset _texts;
    public TextAsset Texts { get { return _texts; } }

    [SerializeField]
    private TMP_FontAsset _fontAsset;
    public TMP_FontAsset FontAsset { get { return _fontAsset; } }
}