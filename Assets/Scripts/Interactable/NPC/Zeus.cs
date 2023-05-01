using Cysharp.Threading.Tasks;
using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Zeus : BaseNPC
{
    private PlayableDirector m_director;
    private bool m_played;
    private void Start()
    {
        m_director = GetComponent<PlayableDirector>();
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
    private void Update()
    {
        if (GameModel.Instance.GameStarted && !m_played)
        {
            m_played = true;
            m_director.Play();
        }
    }
}
