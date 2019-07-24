using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchScreenSpaceButton : MonoBehaviour
{
    private Text text;
    public void SwitchScreen()
    {
        text = text ?? GetComponentInChildren<Text>();
        CanvasManager.Instance.SwitchScreenMode();
        if (CanvasManager.ScreenMode == RenderMode.ScreenSpaceOverlay) text.text = "Screen Space";
        else text.text = "World Space";
    }
}
