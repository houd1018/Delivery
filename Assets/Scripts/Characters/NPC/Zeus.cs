using Isekai.Managers;
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
        PopupManager.Instance.ShowPopup<StartDeliverPopup>(PopupType.StartDeliverPopup, null);
        Game.Instance.PauseGame();
    }
}
