﻿using System.Collections;
using UnityEngine;
using TMPro;

public class SergeantPanelMono : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _textView;


    //Это нужно бы вынести в настройки, но в игре фича с побуквенным выводом так и не была использована
    //, так что не важно...
    [SerializeField]
    private float _letterPrintingDelay;

    private SergeantSpeechController _speechController;

    private string _currentText;
    private int _nextCharIndex;

    private void Start()
    {
        _speechController = Main.Instance.SergeantSpeechController;

        _speechController.OnSayLineRequested += PrintFullText;
        _speechController.OnSpeechLetterByLetterStarted += StartPrintingSequence;
    }

    private void PrintFullText(string text)
    {
        _textView.text = text;
    }


    private void StartPrintingSequence(string text)
    {
        _currentText = text;
        _textView.text = "";
        StartCoroutine(PrintingCoroutine());
    }

    private IEnumerator PrintingCoroutine()
    {
        while (_nextCharIndex < _currentText.Length)
        {
            _textView.text += _currentText[_nextCharIndex++];

            yield return new WaitForSeconds(_letterPrintingDelay);
        }

        _nextCharIndex = 0;
        _speechController.EndLetterByLetterSpeech();
    }

    private void OnDestroy()
    {
        _speechController.OnSayLineRequested -= PrintFullText;
        _speechController.OnSpeechLetterByLetterStarted -= StartPrintingSequence;
    }
}
