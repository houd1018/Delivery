using Isekai.Managers;
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
/*        if (Input.GetKeyDown(KeyCode.S))
        {
            PushMessages(m_dialogueData.Dialogues[m_dialogueData.DialogueCounter].Dialogues);
        }*/
        checkQueueMessages();
    }
    private void checkQueueMessages()
    {
        if (QueuedMessages.Count > 0 && m_messageComplete)
        {
            var message = QueuedMessages.Dequeue();
            m_messageComplete = false;
            m_dialoguePanel.gameObject.SetActive(true);
            m_dialoguePanel.Initialize(message.Text, message.SpeakerName, message.Avatar, () => m_messageComplete = true);
        }
        else if (QueuedMessages.Count == 0 && m_messageComplete)
        {
            m_dialoguePanel.gameObject.SetActive(false);
        }
        
    }

    public void PushNextDialogue()
    {
        PushMessages(m_dialogueData.Dialogues[m_dialogueData.DialogueCounter].Dialogues);
    }
    private void PushMessages(MessageWrapper[] messages)
    {
        foreach (var item in messages)
        {
            QueuedMessages.Enqueue(item); 
        }
        m_messageComplete = true;
    }
    private void OnDestroy()
    {
        Destroy(m_dialoguePanel.gameObject);
    }
}
