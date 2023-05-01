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
    public class GameOverEvent : IEventHandler
    {

    }
    public class PauseScrollEvent : IEventHandler
    {

    }
    public class ResumeScrollEvent : IEventHandler
    {

    }
    public class DialogueEvent : IEventHandler
    {
        public DialogueEvent(bool isTalking)
        {
            IsTalking = isTalking;
        }
        public bool IsTalking;
    }
    public class DepthEvent : IEventHandler
    {
        public float Depth;
    }
    public class CanInteractEvent : IEventHandler
    {
        public CanInteractEvent(bool canInteract)
        {
            CanInteract = canInteract;
        }
        public bool CanInteract;
    }
    public class Game : MonoSingleton<Game>
    {
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
        public void GoToZeusScene()
        {
            LevelManager.Instance.TransitionToScene("ZeusScene",()=> 
            { 
                GameModel.Instance.GameStarted = true;
                var playerdata = Resources.Load<CharacterStats_SO>("Data/CharacterData/PlayerData");
                playerdata.maxHealth = 1;
                playerdata.currentHealth = 1;
/*                ScreenManager.Instance.TransitionToInstant<HUDScreenViewModel>(Isekai.UI.EScreenType.HUDScreen, ELayerType.HUDLayer,
                new HUDScreenViewModel(playerdata)
                {

                });*/
            } ).Forget();
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
            EventSystem.Instance.SendEvent(typeof(PauseScrollEvent), new PauseScrollEvent());
        }
        public void ResumeScroll()
        {
            EventSystem.Instance.SendEvent(typeof(ResumeScrollEvent), new ResumeScrollEvent());
        }
        private void OnPauseClicked()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && GameModel.Instance.GameStarted)
            {
                PauseGame();
                PopupManager.Instance.ShowPopup<PausePopup>(PopupType.PausePopup,
                    new PopupData()
                    {
                        OnCancelClicked = ResumeGame,
                        OnConfirmClicked = OnClickBackToMenu
                    });
            }
        }
        public void OnClickBackToMenu()
        {
            EventSystem.Instance.SendEvent(typeof(GameOverEvent), new GameOverEvent());
            BackToMainMenu().Forget();
        }
        private void Update()
        {
            OnPauseClicked();
            if (Input.GetKeyDown(KeyCode.P))
            {
                PauseScroll();
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                ResumeScroll();
            }
        }

    }

}
