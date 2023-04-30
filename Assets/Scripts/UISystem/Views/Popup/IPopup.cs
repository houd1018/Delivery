using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPopup
{
    PopupData Data { get; set; }
    void OnConfirmClicked();
    void OnCancelClicked();
}
