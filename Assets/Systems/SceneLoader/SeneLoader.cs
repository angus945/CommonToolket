using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.SceneManager
{
    public enum SceneMode
    {
        Clear,
        Single,
        Additive,
        Keep,
    }

    internal enum LoadType
    {
        Load,
        Unload,
    }
    internal class LoadingCommand
    {
        public static LoadingCommand LoadCommand(string sceneName, SceneMode sceneMode)
        {
            return new LoadingCommand()
            {
                sceneName = sceneName,
                sceneMode = sceneMode,
                type = LoadType.Load,
            };
        }
        public static LoadingCommand UnloadCommand(string sceneName)
        {
            return new LoadingCommand()
            {
                sceneName = sceneName,
                type = LoadType.Unload,
            };
        }

        public string sceneName;
        public SceneMode sceneMode;
        public LoadType type;
        public Action finishedCallback;

        AsyncOperation async;
        public bool finished { get => async != null && async.progress == 1; }

        public void Update()
        {
            if (async == null)
            {
                async = CommandStart(type);
            }
        }
        AsyncOperation CommandStart(LoadType loadType)
        {
            switch (loadType)
            {
                case LoadType.Load:
                    return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

                case LoadType.Unload:
                    return UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);
            }

            return null;
        }
        //public void Finishing()
        //{
        //    if (async.progress < 0.9f) return;

        //    async.allowSceneActivation = true;
        //    onLoadedCallback?.Invoke();
        //}
    }

    public struct ActiveScene
    {
        public readonly string sceneName;
        public readonly SceneMode sceneMode;

        public ActiveScene(string sceneName, SceneMode sceneMode)
        {
            this.sceneName = sceneName;
            this.sceneMode = sceneMode;
        }
    }

    public class SeneLoader : MonoBehaviour
    {
        static SeneLoader instance;
        static bool initialized;

        const string TransitionSceneName = "SceneLoader";
        public static void LoadScene(string sceneName, SceneMode sceneMode)
        {
            if (instance == null)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(TransitionSceneName, LoadSceneMode.Additive);
            }
            if (!initialized)
            {
                string defaultScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
                activeScenes.Add(new ActiveScene(defaultScene, SceneMode.Single));
                initialized = true;
            }

            switch (sceneMode)
            {
                case SceneMode.Clear:
                    for (int i = 0; i < activeScenes.Count; i++)
                    {
                        ActiveScene activeScene = activeScenes[i];
                        commands.Add(LoadingCommand.UnloadCommand(activeScene.sceneName));
                    }
                    break;

                case SceneMode.Single:
                    for (int i = 0; i < activeScenes.Count; i++)
                    {
                        ActiveScene activeScene = activeScenes[i];
                        if(activeScene.sceneMode != SceneMode.Keep)
                        {
                            commands.Add(LoadingCommand.UnloadCommand(activeScene.sceneName));
                        }
                    }
                    break;
            }

            commands.Add(LoadingCommand.LoadCommand(sceneName, sceneMode));
        }

        static List<LoadingCommand> commands = new List<LoadingCommand>();
        static List<ActiveScene> activeScenes = new List<ActiveScene>();

        void OnEnable()
        {
            instance = this;
        }
        void OnDisable()
        {
            instance = null;
        }

        void Update()
        {
            CommandUpdate(LoadType.Unload, RemoveScene);
            CommandUpdate(LoadType.Load, AddScene);

            commands.RemoveAll(n => n.finished);
        }

        void CommandUpdate(LoadType updateType, Action<string, SceneMode> onCommandFinished)
        {
            for (int i = 0; i < commands.Count; i++)
            {
                LoadingCommand command = commands[0];

                if (command.type != updateType) continue;
                
                command.Update();
                
                if(command.finished)
                {
                    onCommandFinished.Invoke(command.sceneName, command.sceneMode);
                }
            }
        }
        void AddScene(string sceneName, SceneMode sceneMode)
        {
            activeScenes.Add(new ActiveScene(sceneName, sceneMode));
        }
        void RemoveScene(string sceneName, SceneMode sceneMode)
        {
            Debug.Log(sceneName);
            activeScenes.RemoveAll(n => n.sceneName == sceneName);
        }
        //void UnloadTransitionScene()
        //{
        //    if (commands.Count != 0) return;

        //    UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(TransitionSceneName);
        //    gameObject.SetActive(false);
        //}
    }
}