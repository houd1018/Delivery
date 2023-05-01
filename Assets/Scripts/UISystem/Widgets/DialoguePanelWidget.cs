using Cysharp.Threading.Tasks;
using MyPackage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePanelWidget : MonoBehaviour
{
    public Action OnNextMessage;

    [SerializeField]
    private Image m_leftAvatar;
    [SerializeField]
    private TextMeshProUGUI m_talkText;
    [SerializeField]
    private TextMeshProUGUI m_speakerName;
    [SerializeField]
    private float m_textSpeed;

    private CancellationTokenSource m_tokenSource;
    private string curText;
    private bool m_complete;

    private void Start()
    {
    }
    private void Update()
    {
        checkUserInput();
    }
    public void Initialize(string talkText, string speakerName, Sprite avatar, Action onMessageComplete)
    {
        m_leftAvatar.gameObject.SetActive(true);
        m_leftAvatar.sprite = avatar;
        m_speakerName.text = speakerName;

        curText = talkText;
        m_talkText.text = curText;
        
        this.OnNextMessage += onMessageComplete;
        //StartShowText
        if (m_tokenSource != null)
        {
            m_tokenSource.Cancel();
            m_tokenSource.Dispose();
        }
        m_tokenSource = new CancellationTokenSource();
        m_complete = false;
        SetTalkText().Forget();

    }

    public async UniTaskVoid SetTalkText()
    {
        for (int i = 0; i < curText.Length; i++)
        {
            m_talkText.text = curText.Substring(0, i + 1);
            await UniTask.Delay(TimeSpan.FromSeconds(m_textSpeed),true,PlayerLoopTiming.Update, m_tokenSource.Token);
        }
        UniTask.ReturnToMainThread(this.GetCancellationTokenOnDestroy());
        m_complete = true;
    }

    public void OnClickSkip()
    {
        DialogueManager.Instance.SkipDialogue();
        OnNextMessage?.Invoke();
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_icon_hover);
    }
    private void checkUserInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!m_complete)
            {
                m_tokenSource.Cancel();
                m_talkText.text = curText;
                m_complete = true;

                SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_icon_hover);
            }
            else
            {
                OnNextMessage?.Invoke();
                SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_icon_hover);
            }
        }
    }
}
