using System;
using UnityEngine;
using TMPro;

using LanguagesAndTexts;

public abstract class TextsOutputMono_Base : MonoBehaviour
{
    private LanguageDependentTextsController _textsController;

    private void Start()
    {
        _textsController = Main.Instance.LanguageDependentTextsController;        
        _textsController.OnTextsChanged += SetTextValues;

        Initialize();

        SetTextValues(_textsController.Texts, _textsController.Font);
    }

    private void OnDestroy()
    {
        _textsController.OnTextsChanged -= SetTextValues;

        OnGameobjectDestroyed();
    }

    protected virtual void Initialize()
    {
    }

    protected virtual void SetTextValues(Texts texts, TMP_FontAsset font)
    {
        throw new NotImplementedException();
    }

    protected virtual void OnGameobjectDestroyed()
    {
    }
}