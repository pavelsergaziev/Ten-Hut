using UnityEngine;

[CreateAssetMenu(fileName = "SceneLoadingSettings", menuName = "GameScriptableObjectsAsset/SceneLoadingSettings")]
public class SceneLoadingSettingsSO : ScriptableObject
{
    [SerializeField] private int[] _scenesRequired;
    [SerializeField] private bool _unloadAllNonEssentialScenes;

    public int[] ScenesRequired { get { return _scenesRequired; } }
    public bool UnloadAllNonEssentialScenes { get { return _unloadAllNonEssentialScenes; } }
}
