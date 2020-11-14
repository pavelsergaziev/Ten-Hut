using System;
using UnityEngine;

namespace LanguagesAndTexts
{
    using TMPro;

    public class LanguageDependentTextsController
    {
        public Texts Texts { get; private set; }
        public TMP_FontAsset Font { get; private set; }

        public event Action<Texts, TMP_FontAsset> OnTextsChanged = delegate { };

        public void LoadTextsInLanguage(Languages targetLanguage)
        {
            Texts = JsonUtility.FromJson<Texts>(Main.Instance.Settings.LanguagesDictionary.GetValueByKey(targetLanguage).Texts.text);
            Font = Main.Instance.Settings.LanguagesDictionary.GetValueByKey(targetLanguage).FontAsset;

            OnTextsChanged.Invoke(Texts, Font);
        }
    }
}