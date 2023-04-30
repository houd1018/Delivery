using Isekai.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePopup : MonoBehaviour, IPopup
{
    public PopupData Data { get; set; }

    public void OnCancelClicked()
    {
        Data.OnCancelClicked();
        Destroy(gameObject);
    }

    public void OnConfirmClicked()
    {
        Data.OnConfirmClicked();
        Destroy(gameObject);
    }
}
