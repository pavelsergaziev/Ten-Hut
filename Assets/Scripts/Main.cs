using System.Collections.Generic;
using UnityEngine;


[DefaultExecutionOrder(-2000)]
public class Main : MonoBehaviour
{
    [SerializeField] private SettingsSO _settings;
    public SettingsSO Settings { get { return _settings; } }

    public static Main Instance { get; private set; }

    public Updater Updater { get; private set; }
    public SceneLoadingController SceneLoader { get; private set; }

    public PlayerInput.IPlayerInput PlayerInputController { get; private set; }        
    public GameStateController GameStateController { get; private set; }
    public UIButtonEffectsController UIButtonEffectsController { get; private set; }

    public OrdersAndExecution.OrdersAndExecutionGameLoopController OrdersAndExecutionGameLoop { get; private set; }
    public OrdersAndExecution.PlayerActionsInputController PlayerActionsController { get; private set; }    
    public SergeantSpeechController SergeantSpeechController { get; private set; }
    public DrillSergeantController DrillSergeantController { get; private set; }
    public SergeantAnimationController SergeantAnimationController { get; private set; }
    public SoldierAnimationController SoldierAnimationController { get; private set; }
    public AudioFXDispatcherController CommonAudioFXDispatcher { get; private set; }
    public AudioFXPlayerController SergeantAudioFXController { get; private set; }
    public AudioFXPlayerController CommonAudioFXController { get; private set; }
    public ScoreController ScoreController { get; private set; }
    public PlayerHealthController PlayerHealthController { get; private set; }


    private List<IDependencyInjectionReceiver> _dIReceivers;

    private void Awake()
    {
        CreateSingleton();

        _dIReceivers = new List<IDependencyInjectionReceiver>();

        CreateMonobehaviorControllers();
        CreateNonMonobehaviorControllers();
    }

    private void Start()
    {
        InjectDependencies();
    }

    private void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CreateMonobehaviorControllers()
    {
        Updater = gameObject.AddComponent<Updater>();
    }

    private void CreateNonMonobehaviorControllers()
    {
        SceneLoader = new SceneLoadingController();
        GameStateController = new GameStateController();
        CommonAudioFXController = new AudioFXPlayerController();
        CommonAudioFXDispatcher = new AudioFXDispatcherController();
        UIButtonEffectsController = new UIButtonEffectsController();

#if UNITY_WEBGL
        PlayerInputController = new PlayerInput.PlayerInputController_Keyboard();
#endif
#if UNITY_STANDALONE
        PlayerInputController = new PlayerInput.PlayerInputController_Keyboard();
#endif
#if UNITY_ANDROID
        //PlayerInput = new PlayerInput.PlayerInputController_Mobile();
#endif

        PlayerActionsController = new OrdersAndExecution.PlayerActionsInputController();
        OrdersAndExecutionGameLoop = new OrdersAndExecution.OrdersAndExecutionGameLoopController();
        SergeantSpeechController = new SergeantSpeechController();
        DrillSergeantController = new DrillSergeantController();
        SergeantAnimationController = new SergeantAnimationController();
        SergeantAudioFXController = new AudioFXPlayerController();
        ScoreController = new ScoreController();
        PlayerHealthController = new PlayerHealthController();
        SoldierAnimationController = new SoldierAnimationController();        
    }

    public void SubscribeToDependencyInjection(IDependencyInjectionReceiver target)
    {
        _dIReceivers.Add(target);
    }

    private void InjectDependencies()
    {
        foreach (var target in _dIReceivers)
        {
            target.InjectDependencies();
        }
    }

}
