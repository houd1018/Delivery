using Isekai.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType
{
    PausePopup,
    StartDeliverPopup,
}
public class PopupData
{
    public Action OnCancelClicked;
    public Action OnConfirmClicked;
}
public class PopupManager : MonoSingleton<PopupManager>
{
    public IPopup ShowPopup<T>(PopupType Type, PopupData data)
    {
        var prefab = Resources.Load<GameObject>("Prefabs/UI/Popup/" + Type.ToString());
        IPopup popup = Instantiate(prefab, LayerManager.Instance.GetLayer(ELayerType.PopupLayer)).GetComponent<IPopup>();
        popup.Data = data;
        return popup;
    }
}
