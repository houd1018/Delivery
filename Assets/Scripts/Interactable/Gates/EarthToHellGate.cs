using Cysharp.Threading.Tasks;
using Isekai.Managers;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthToHellGate : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GotoHell();
    }
    void GotoHell()
    {
        StartDeliverPopup popup = PopupManager.Instance.ShowPopup<StartDeliverPopup>(PopupType.StartDeliverPopup,
        new PopupData()
        {
            OnCancelClicked = () => { Game.Instance.ResumeGame(); },
            OnConfirmClicked = onConfirm
        });
        popup.SetTitle("Go To Hell?");
        Game.Instance.PauseGame();
    }
    void onConfirm()
    {
        Game.Instance.ResumeGame();
        GameModel.Instance.GameStarted = false;
        LevelManager.Instance.TransitionToScene("Hell", () =>
        {
            EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
            var playerdata = Resources.Load<CharacterStats_SO>("Data/CharacterData/PlayerData");
            ScreenManager.Instance.TransitionToInstant<HUDScreenViewModel>(Isekai.UI.EScreenType.HUDScreen, ELayerType.HUDLayer,
            new HUDScreenViewModel(playerdata)
            {

            });
        },5,"Tips: You can press <shift> to TELEPORT to another block").Forget();
    }
}
