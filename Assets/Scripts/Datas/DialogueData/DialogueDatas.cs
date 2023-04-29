using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DialogueDatas",menuName ="Data/DialogueDatas",order =2)]
public class DialogueDatas : ScriptableObject
{
    public int DialogueCounter;
    public DialogueData[] Dialogues;
}
