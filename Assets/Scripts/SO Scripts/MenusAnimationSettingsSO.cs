using UnityEngine;

[CreateAssetMenu(fileName = "MenusAnimationSettings", menuName = "GameScriptableObjectsAsset/MenusAnimation")]
public class MenusAnimationSettingsSO : ScriptableObject
{
    [SerializeField] private float _movementDuration;

    public float MovementDuration { get { return _movementDuration; } }
}
