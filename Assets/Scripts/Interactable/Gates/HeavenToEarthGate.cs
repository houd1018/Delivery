using Cysharp.Threading.Tasks;
using Isekai.Managers;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavenToEarthGate :MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GotoEarth();
    }
    void GotoEarth()
    {
        StartDeliverPopup popup = PopupManager.Instance.ShowPopup<StartDeliverPopup>(PopupType.StartDeliverPopup, 
        new PopupData()
        {
            OnCancelClicked = () => { Game.Instance.ResumeGame(); },
            OnConfirmClicked = onConfirm
        });
        popup.SetTitle("Go To Earth?");
        Game.Instance.PauseGame();
    }
    void onConfirm()
    {
        Game.Instance.ResumeGame();
        GameModel.Instance.GameStarted = false;
        LevelManager.Instance.TransitionToScene("Earth", () =>
        {
            var playerdata = Resources.Load<CharacterStats_SO>("Data/CharacterData/PlayerData");
            ScreenManager.Instance.TransitionToInstant<HUDScreenViewModel>(Isekai.UI.EScreenType.HUDScreen, ELayerType.HUDLayer,
            new HUDScreenViewModel(playerdata)
            {

            });
            EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
        }).Forget();
    }
}
