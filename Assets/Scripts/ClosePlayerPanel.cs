using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClosePlayerPanel : MonoBehaviour
{
    public void ClosePanelPlayer()
    {
        PlayerInfoPanel.Instance.HideInfo();
    }
}
