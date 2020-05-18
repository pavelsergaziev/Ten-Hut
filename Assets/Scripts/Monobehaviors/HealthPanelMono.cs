using UnityEngine;
using TMPro;
using System;

public class HealthPanelMono : MonoBehaviour
{
    [SerializeField]
    private RectTransform _healthBarParent;

    private GameObject[] _healthBarElements; 

    private PlayerHealthController _controller;

    private void Start()
    {       
        _controller = Main.Instance.PlayerHealthController;
        PopulateHealthBar();
        _controller.OnCurrentHealthChanged += ChangeHealth;
    }


    private void PopulateHealthBar()
    {
        float anchoredPositionX = 0f;
        _healthBarElements = new GameObject[_controller.MaxHealth];

        for (int i = 0; i < _healthBarElements.Length; i++)
        {
            _healthBarElements[i] = Instantiate(Main.Instance.Settings.HealthPanelSettings.HealthBarElementPrefab, _healthBarParent);
            RectTransform element = _healthBarElements[i].GetComponent<RectTransform>();
            anchoredPositionX = ((element.rect.size.x)/2) + (i * element.rect.size.x * Main.Instance.Settings.HealthPanelSettings.ElementWithOffsetSizeX);
            element.anchoredPosition = new Vector2(anchoredPositionX, element.anchoredPosition.y);            
        }       

    }

    private void ChangeHealth(int currentHealth)
    {
        for (int i = 0; i < _healthBarElements.Length; i++)
        {
            if (currentHealth > i)
            {
                _healthBarElements[i].SetActive(true);
            }
            else
            {
                _healthBarElements[i].SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        _controller.OnCurrentHealthChanged -= ChangeHealth;
    }
}
