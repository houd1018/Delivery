using Cysharp.Threading.Tasks;
using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hades : BaseNPC
{
    private void Start()
    {
        this.OnDialoguesComplete += ShowGameWinPopup;
    }
    public void ShowGameWinPopup()
    {
        StartDeliverPopup popup = PopupManager.Instance.ShowPopup<StartDeliverPopup>(PopupType.StartDeliverPopup,
            new PopupData()
            {
                OnCancelClicked = () => { Game.Instance.OnClickBackToMenu(); },
                OnConfirmClicked = onConfirm
            });
        popup.SetTitle("You Complete the DELIVER");
        popup.SetConfirmButton("Retry");
        popup.SetCancelButton("Back");
        Game.Instance.PauseGame();
    }
    void onConfirm()
    {
        Game.Instance.ResumeGame();
        GameModel.Instance.GameStarted = false;
        LevelManager.Instance.TransitionToScene("ZeusScene", () =>
        {
            EventSystem.Instance.SendEvent(typeof(GameOverEvent), new GameOverEvent());
            EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
        },0,"").Forget();
    }
}
