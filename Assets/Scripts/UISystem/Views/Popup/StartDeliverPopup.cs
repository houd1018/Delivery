using Cysharp.Threading.Tasks;
using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDeliverPopup :MonoBehaviour, IPopup
{
    public PopupData Data { get; set; }

    public void OnCancelClicked()
    {
        Game.Instance.ResumeGame();
        Destroy(gameObject);
    }

    public void OnConfirmClicked()
    {
        Game.Instance.ResumeGame();
        GameModel.Instance.GameStarted = false;
        LevelManager.Instance.TransitionToScene("Heaven", () =>
        {
            EventSystem.Instance.SendEvent(typeof(GameStartEvent), new GameStartEvent());
        }).Forget();
        Destroy(gameObject);
    }


}
