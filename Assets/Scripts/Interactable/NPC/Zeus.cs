using Cysharp.Threading.Tasks;
using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : BaseNPC
{
    private void Start()
    {
        this.OnDialoguesComplete += GoToPlayScenePopup;
        this.OnRepeatDialogueComplete += GoToPlayScenePopup;
    }
    public void GoToPlayScenePopup()
    {
        PopupManager.Instance.ShowPopup<StartDeliverPopup>(PopupType.StartDeliverPopup, 
            new PopupData() 
            {
                OnCancelClicked = () => { Game.Instance.ResumeGame();},
                OnConfirmClicked = onConfirm  
            });
        Game.Instance.PauseGame();
    }
    void onConfirm()
    {
        Game.Instance.ResumeGame();
        GameModel.Instance.GameStarted = false;
        LevelManager.Instance.TransitionToScene("Heaven", () =>
        {
            EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
        }).Forget();
    }
}
