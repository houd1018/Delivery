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
    public T ShowPopup<T>(PopupType Type, PopupData data) where T:MonoBehaviour,IPopup
    {
        var prefab = Resources.Load<GameObject>("Prefabs/UI/Popup/" + Type.ToString());
        T popup = Instantiate(prefab, LayerManager.Instance.GetLayer(ELayerType.PopupLayer)).GetComponent<T>();
        popup.Data = data;
        return popup;
    }
}
