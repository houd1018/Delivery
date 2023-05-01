using Cysharp.Threading.Tasks;
using Isekai.Managers;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Isekai.UI.Views.Screens
{
    public class GameOverScreen : Screen<GameOverViewModel>
    {
        public override void OnEnterScreen()
        {
        }

        public override void OnExitScreen()
        {
        }
        public void OnClickPlayAgain()
        {
            LevelManager.Instance.TransitionToScene("Heaven", () =>
            {
                EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
                var playerdata = Resources.Load<CharacterStats_SO>("Data/CharacterData/PlayerData");
                playerdata.currentHealth = 1;
                playerdata.maxHealth = 1;
                ScreenManager.Instance.TransitionToInstant<HUDScreenViewModel>(Isekai.UI.EScreenType.HUDScreen, ELayerType.HUDLayer,
                new HUDScreenViewModel(playerdata)
                {

                });
            }).Forget();
        }
        public void OnClickBackToMainMenu()
        {
            Game.Instance.BackToMainMenu().Forget();
        }
    }

}
