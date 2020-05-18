using UnityEngine;

[CreateAssetMenu(fileName = "HealthPanelSettings", menuName = "GameScriptableObjectsAsset/HealthPanelSettings")]
public class HealthPanelSettingsSO : ScriptableObject
{
    [SerializeField] private GameObject _healthBarElementPrefab;
    [SerializeField] private float _elementWithOffsetSizeX;

    public GameObject HealthBarElementPrefab { get { return _healthBarElementPrefab; } }
    public float ElementWithOffsetSizeX { get { return _elementWithOffsetSizeX; } }
}
