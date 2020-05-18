using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SceneLoadingController
{
    public event Action OnScenesFinishedLoading = delegate { };

    private int _asyncOperationsCurrentlyRunningCount;

    private List<int> _getLoadedScenes
    { get
        {
            List<int> result = new List<int>();

            for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (SceneManager.GetSceneByBuildIndex(i).isLoaded)
                {
                    result.Add(i);
                }
            }

            return result;
        }
    }


    public void LoadScenes(SceneLoadingSettingsSO sceneSettings)
    {
        if (sceneSettings.UnloadAllNonEssentialScenes)
        {
            var loadedNonEssentialScenes = _getLoadedScenes.Except(sceneSettings.ScenesRequired);

            foreach (var scene in loadedNonEssentialScenes)
            {
                _asyncOperationsCurrentlyRunningCount++;
                SceneManager.UnloadSceneAsync(scene).completed += UpdateLoadingStatus;
            }
        }

        var scenesThatNeedToBeLoaded = sceneSettings.ScenesRequired.Except(_getLoadedScenes);

        foreach (var scene in scenesThatNeedToBeLoaded)
        {
            _asyncOperationsCurrentlyRunningCount++;
            SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive).completed += UpdateLoadingStatus;
        }

    }

    private void UpdateLoadingStatus(AsyncOperation operation)
    {
        if (--_asyncOperationsCurrentlyRunningCount <= 0)
        {
            OnScenesFinishedLoading.Invoke();
        }
    }
}
