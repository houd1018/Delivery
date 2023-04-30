using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractPromptManager : MonoSingleton<InteractPromptManager>
{
    public TextMeshProUGUI Prompt;

    private void Start()
    {
        EventSystem.Instance.Subscribe<GameStartEvent>(typeof(GameStartEvent), onGameStart);
    }
    public void ShowPrompt(string text, Vector3 pos)
    {
        Prompt.gameObject.SetActive(true);
        Prompt.transform.position = pos;
        Prompt.text = text;
    }
    public void HidePrompt()
    {
        Prompt.gameObject.SetActive(false);
    }

    void onGameStart(GameStartEvent e)
    {
        HidePrompt();
    }
    private void OnDestroy()
    {
        EventSystem.Instance.Unsubscribe<GameStartEvent>(typeof(GameStartEvent), onGameStart);
    }
}
