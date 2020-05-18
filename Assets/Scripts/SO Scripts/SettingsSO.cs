using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllSettings", menuName = "GameScriptableObjectsAsset/Settings")]
public class SettingsSO : ScriptableObject
{
    [SerializeField]
    private OrdersAndExecutionSettingsSO _ordersAndExecutionSettings;
    public OrdersAndExecutionSettingsSO OrdersAndExecutionSettings { get { return _ordersAndExecutionSettings; } }

    [SerializeField]
    private HighScoreSO _highScoreSettings;
    public HighScoreSO HighScoreSettings { get { return _highScoreSettings; } }

    [SerializeField]
    private ScoreCountingSO _scoreCountingSettings;
    public ScoreCountingSO ScoreCountingSettings { get { return _scoreCountingSettings; } }
    
    [SerializeField]
    private ScenesDictionarySO _sceneLoadingSettings;
    public ScenesDictionarySO SceneLoadingSettings { get { return _sceneLoadingSettings; } }

    [SerializeField]
    private CommonAudioFXSettingsSO _commonAudioFXSettings;
    public CommonAudioFXSettingsSO CommonAudioFXSettings { get { return _commonAudioFXSettings; } }

    [SerializeField]
    private SoldierAnimationsSO _soldierAnimations;
    public SoldierAnimationsSO SoldierAnimations { get { return _soldierAnimations; } }

    [SerializeField]
    private SergeantAnimationSettngsSO _sergeantAnimation;
    public SergeantAnimationSettngsSO SergeantAnimation { get { return _sergeantAnimation; } }

    [SerializeField]
    private MenusAnimationSettingsSO _menusAnimationSettings;
    public MenusAnimationSettingsSO MenusAnimationSettings { get { return _menusAnimationSettings; } }

    [SerializeField]
    private UIShakeSettingsSO _uIShakeSettings;
    public UIShakeSettingsSO UIShakeSettings { get { return _uIShakeSettings; } }

    [SerializeField]
    private HealthPanelSettingsSO _healthPanelSettings;
    public HealthPanelSettingsSO HealthPanelSettings { get { return _healthPanelSettings; } }
}
