using Cysharp.Threading.Tasks;
using Isekai.Managers;
using MyPackage;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartDeliverPopup :MonoBehaviour, IPopup
{
    [SerializeField]
    private TextMeshProUGUI m_title;
    [SerializeField]
    private TextMeshProUGUI m_confirmText;
    [SerializeField]
    private TextMeshProUGUI m_cancelText;
    public PopupData Data { get; set; }
    public void SetTitle(string text)
    {
        m_title.text = text;
    }
    public void SetConfirmButton(string text)
    {
        m_confirmText.text = text;
    }
    public void SetCancelButton(string text)
    {
        m_cancelText.text = text;
    }
    public void OnCancelClicked()
    {
        Data.OnCancelClicked?.Invoke();
        Destroy(gameObject);
    }

    public void OnConfirmClicked()
    {
        Data.OnConfirmClicked?.Invoke();
        Destroy(gameObject);
    }

}
