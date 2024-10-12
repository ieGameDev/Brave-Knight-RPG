using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SceneLoader
    {
        public void LoadScene(string sceneName, Action onLoaded = null)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                onLoaded?.Invoke();
                return;
            }

            AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(sceneName);
            
            if (waitNextScene != null) 
                waitNextScene.completed += _ => onLoaded?.Invoke();
        }
    }
}