using Cysharp.Threading.Tasks;
using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthToHellGate : MonoBehaviour, IInteractable
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
        }).Forget();
    }
}
