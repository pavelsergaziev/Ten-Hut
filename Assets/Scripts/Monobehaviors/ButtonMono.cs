using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMono : MonoBehaviour
{
    [SerializeField]
    private MenuButtons _buttonValue;

    private UIButtonEffectsController _controller;

    private void Start()
    {
        _controller = Main.Instance.UIButtonEffectsController;
    }

    public void PressButton()
    {
        _controller.ButtonEffectRequested(_buttonValue);
    }
}
