using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNPC : MonoBehaviour,IInteractable
{
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
            DialogueManager.Instance.PushMessages(data.Dialogues, OnDialoguesComplete);
        }
        else
        {
            DialogueManager.Instance.PushMessages(RepeatDialogue.Dialogues, OnRepeatDialogueComplete);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
