using Cysharp.Threading.Tasks;
using Isekai.Managers;
using Isekai.UI.ViewModels.Screens;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Zeus : BaseNPC
{
    
    private bool m_played;
    protected override void Start()
    {
        base.Start();
        this.OnDialoguesComplete += GoToPlayScenePopup;
        this.OnRepeatDialogueComplete += GoToPlayScenePopup;
    }
    public void GoToPlayScenePopup()
    {
        var playerdata = Resources.Load<CharacterStats_SO>("Data/CharacterData/PlayerData");
        playerdata.currentHealth = 1;
        playerdata.maxHealth = 1;
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
            var playerdata = Resources.Load<CharacterStats_SO>("Data/CharacterData/PlayerData");
            ScreenManager.Instance.TransitionToInstant<HUDScreenViewModel>(Isekai.UI.EScreenType.HUDScreen, ELayerType.HUDLayer,
            new HUDScreenViewModel(playerdata)
            {

            });
        },3, "Tips: Use WASD to move and click to continue with dialogues").Forget();
    }
    public void PlayEatAppleSound()
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_apple);
    }
    protected override void Update()
    {
        base.Update();
        if (GameModel.Instance.GameStarted && !m_played)
        {
            m_played = true;
            m_director.Play();
        }
    }
}
