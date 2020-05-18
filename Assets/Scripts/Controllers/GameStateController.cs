using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using OrdersAndExecution;
using System;

public class GameStateController: IDependencyInjectionReceiver
{
    private Scenes _currentGameState;

    private SceneLoadingController _sceneLoader;
    private OrdersAndExecutionGameLoopController _gameplayLoopController;
    private UIButtonEffectsController _uIButtonEffectsController;
    private ScoreController _scoreController;

    public GameStateController()
    {        
        Main.Instance.SubscribeToDependencyInjection(this);
    }

    public void InjectDependencies()
    {
        _gameplayLoopController = Main.Instance.OrdersAndExecutionGameLoop;
        _sceneLoader = Main.Instance.SceneLoader;
        _uIButtonEffectsController = Main.Instance.UIButtonEffectsController;
        _scoreController = Main.Instance.ScoreController;

        _uIButtonEffectsController.OnButtonPressed += ResolveButtonPressed;

        EnterInitialGameState();
    }

    private void EnterInitialGameState()
    {        
        LoadMainMenu();
    }

    private void ResolveButtonPressed(MenuButtons button)
    {
        if (button == MenuButtons.StartGame)
        {
            LoadGameplay();
        }

        if (button == MenuButtons.ReturnToPreviousScreen && _currentGameState == Scenes.gameoverScreen)
        {
            LoadMainMenu();
        }

        if (button == MenuButtons.ExitGame)
        {
            Application.Quit();
        }
    }

    private void LoadMainMenu()
    {
        _currentGameState = Scenes.mainMenu;
        LoadScenesPack();
    }

    private void LoadGameplay()
    {
        _currentGameState = Scenes.gameplay;
        LoadScenesPack();
        _sceneLoader.OnScenesFinishedLoading += LaunchGameplayLoop;
    }

    private void LaunchGameplayLoop()
    {
        _sceneLoader.OnScenesFinishedLoading -= LaunchGameplayLoop;
        _scoreController.ResetCurrentScore();
        _gameplayLoopController.LaunchGame();
        _gameplayLoopController.OnGameOver += ResolveOnGameOver;
    }

    private void ResolveOnGameOver()
    {
        _gameplayLoopController.OnGameOver -= ResolveOnGameOver;
        _scoreController.SaveNewHighScore();

        _currentGameState = Scenes.gameoverScreen;
        LoadScenesPack();
    }

    private void LoadScenesPack()
    {
        _sceneLoader.LoadScenes(Main.Instance.Settings.SceneLoadingSettings.GetValueByKey(_currentGameState));
    }
    
}
