using UnityEngine;

[CreateAssetMenu(fileName = "ASceneLoadingSettings", menuName = "GameScriptableObjectsAsset/SceneLoadingSettings")]
public class SceneLoadingSettingsSO : ScriptableObject
{
    [SerializeField] private int[] _scenesRequired;
    [SerializeField] private bool _unloadAllNonEssintialScenes;

    public int[] ScenesRequired { get { return _scenesRequired; } }
    public bool UnloadAllNonEssentialScenes { get { return _unloadAllNonEssintialScenes; } }
}
