using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonSoundWidget : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Confirm);

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
    }
}
