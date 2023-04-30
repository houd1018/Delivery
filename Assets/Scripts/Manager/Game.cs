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
    public class ResumeGameEvent : IEventHandler
    {

    }

    public class GameStartEvent : IEventHandler
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
            Time.timeScale = 1;
            await LevelManager.Instance.TransitionToScene("MainMenu",null);
            ScreenManager.Instance.TransitionToInstant(UI.EScreenType.MainMenuScreen, ELayerType.DefaultLayer, new MainMenuViewModel());
        }
        public void GoToPlayScene()
        {
            LevelManager.Instance.TransitionToScene("PlayScene",
                () => 
                {
                    GameStarted = true;
                    EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
                    //TODO delete this test call;
                    DialogueManager.Instance.PushNextDialogue();
                }).Forget();
        }
        public void PauseGame()
        {
            Time.timeScale = 0;
            EventSystem.Instance.SendEvent(typeof(PauseGameEvent), new PauseGameEvent());
        }
        public void ResumeGame()
        {
            Time.timeScale = 1;
            EventSystem.Instance.SendEvent(typeof(ResumeGameEvent), new ResumeGameEvent());
        }
        public void PauseScroll()
        {
            
        }
        public void ResumeScroll()
        {

        }
        private void Update()
        {

        }

    }

}
