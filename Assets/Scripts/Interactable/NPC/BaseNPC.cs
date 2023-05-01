using Isekai.Managers;
using MyPackage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BaseNPC : MonoBehaviour,IInteractable
{
    protected PlayableDirector m_director;
    public List<DialogueData> DialoguesQueue;
    public DialogueData RepeatDialogue;

    public Action OnDialoguesComplete;
    public Action OnRepeatDialogueComplete;
    public void Interact()
    {

            if (DialoguesQueue.Count > 0)
            {
                DialogueData data = DialoguesQueue[0];
                DialoguesQueue.RemoveAt(0);
                if (DialoguesQueue.Count == 0)
                {
                    DialogueManager.Instance.PushMessages(data.Dialogues, OnDialoguesComplete);
                }
                else
                {
                    DialogueManager.Instance.PushMessages(data.Dialogues, null);
                }
            }
            else
            {
                DialogueManager.Instance.PushMessages(RepeatDialogue.Dialogues, OnRepeatDialogueComplete);
            }
        

    }
    private void Awake()
    {
        EventSystem.Instance.Subscribe<PauseGameEvent>(typeof(PauseGameEvent), onGamePause);
        EventSystem.Instance.Subscribe<ResumeGameEvent>(typeof(ResumeGameEvent), onGameResume);
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        m_director = GetComponent<PlayableDirector>();

    }
    void onGamePause(PauseGameEvent e)
    {
        if (m_director != null)
        {
            m_director.Pause();
        }
       
    }
    void onGameResume(ResumeGameEvent e)
    {
        if (m_director != null)
        {
            m_director.Resume();
        }
        
    }

    public void InTimeLine()
    {
        GameModel.Instance.InTimeLine = true;
        GameModel.Instance.CanInteract = false;
    }
    public void OutOfTimeLine()
    {
        GameModel.Instance.InTimeLine = false;
        GameModel.Instance.CanInteract = true;
    }
    // Update is called once per frame
    protected virtual void Update()
    {

    }
    private void OnDestroy()
    {
        EventSystem.Instance.Unsubscribe<PauseGameEvent>(typeof(PauseGameEvent), onGamePause);
        EventSystem.Instance.Unsubscribe<ResumeGameEvent>(typeof(ResumeGameEvent), onGameResume);
    }
}
