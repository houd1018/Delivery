using Isekai.Managers;
using MyPackage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MessageWrapper
{
    public string SpeakerName;
    public string Text;
    public Sprite Avatar;
}
public class DialogueManager : MonoSingleton<DialogueManager>
{
    [SerializeField]
    private DialoguePanelWidget m_dialoguePanel;
    [SerializeField]
    private DialogueDatas m_dialogueData;

    public Queue<MessageWrapper> QueuedMessages;

    private bool m_messageComplete;
    private Action m_curOnMessageComplete;
    void Start()
    {
        QueuedMessages = new Queue<MessageWrapper>();
        var prefab = Resources.Load<GameObject>("Prefabs/UI/Widgets/DialoguePanelWidget");
        m_dialoguePanel = Instantiate(prefab, LayerManager.Instance.GetLayer(ELayerType.DefaultLayer)).GetComponent<DialoguePanelWidget>();
        m_dialoguePanel.gameObject.SetActive(false);
    }
    public void Initialize()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        checkQueueMessages();
    }
    private void checkQueueMessages()
    {
        if (QueuedMessages.Count > 0 && m_messageComplete)
        {
            Game.Instance.PauseGame();
            EventSystem.Instance.SendEvent(typeof(DialogueEvent), new DialogueEvent(true));
            var message = QueuedMessages.Dequeue();
            m_messageComplete = false;
            m_dialoguePanel.gameObject.SetActive(true);
            m_dialoguePanel.Initialize(message.Text, message.SpeakerName, message.Avatar, () => m_messageComplete = true);
        }
        else if (QueuedMessages.Count == 0 && m_messageComplete)
        {
            m_messageComplete = false;
            Game.Instance.ResumeGame();
            EventSystem.Instance.SendEvent(typeof(DialogueEvent), new DialogueEvent(false));
            m_dialoguePanel.gameObject.SetActive(false);

            m_curOnMessageComplete?.Invoke();
            m_curOnMessageComplete = default;
        }

    }

    public void PushNextDialogue()
    {
        //PushMessages(m_dialogueData.Dialogues[m_dialogueData.DialogueCounter].Dialogues);
    }

    public void PushMessages(MessageWrapper[] messages,Action onDialogueComplete)
    {
        foreach (var item in messages)
        {
            QueuedMessages.Enqueue(item); 
        }
        m_messageComplete = true;
        m_curOnMessageComplete += onDialogueComplete;
    }
    public void SkipDialogue()
    {
        QueuedMessages.Clear();
    }
    private void OnDestroy()
    {
        if (m_dialoguePanel != null)
        {
            Destroy(m_dialoguePanel.gameObject);
        }
    }
}
