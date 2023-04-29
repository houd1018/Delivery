using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DialogueData",menuName ="Data/DialogueData",order =3)]
public class DialogueData : ScriptableObject
{
    public MessageWrapper[] Dialogues;

}
