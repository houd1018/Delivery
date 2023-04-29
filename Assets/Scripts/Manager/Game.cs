using Cysharp.Threading.Tasks;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.Managers
{
    public class PauseGameEvent : IEventHandler
    {

    }
    public class Game : MonoSingleton<Game>
    {
        public bool GameStarted;
        async void Start()
        {
            await InitializeManagers();

            MainMenuViewModel viewmodel = new MainMenuViewModel();
            ScreenManager.Instance.TransitionToInstant(UI.EScreenType.MainMenuScreen,ELayerType.DefaultLayer, viewmodel);

            SoundManager.Instance.PlayMusic(SoundDefine.Music_Login);
        }

        public async UniTask InitializeManagers()
        {
            LayerManager.Instance.Initialize();
            ScreenManager.Instance.Initialize();
            SoundManager.Instance.Initialize();
            LevelManager.Instance.Initialize();
        }
        public async UniTaskVoid BackToMainMenu()
        {
            await LevelManager.Instance.TransitionToScene("MainMenu",null);
            ScreenManager.Instance.TransitionToInstant(UI.EScreenType.MainMenuScreen, ELayerType.DefaultLayer, new MainMenuViewModel());
        }
        public void GoToPlayScene()
        {
            LevelManager.Instance.TransitionToScene("PlayScene",
                () => 
                {
                    GameStarted = true;
                    //TODO delete this test call;
                    DialogueManager.Instance.PushNextDialogue();
                }).Forget();
        }
        public void PauseGame()
        {
            GameStarted = false;
        }
        public void ResumeGame()
        {
            GameStarted = true;
        }
        private void Update()
        {

        }

    }

}
