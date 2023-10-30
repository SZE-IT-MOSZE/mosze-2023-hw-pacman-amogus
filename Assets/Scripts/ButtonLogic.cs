using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public enum ButtonType
    {
        Save,
        Load
    }
    public ButtonType type;

    public int buttonIndex;

    public void ButtonPressed()
    {
        switch (type)
        {
            case ButtonType.Save:
                UILogic.instance.SaveSlot(buttonIndex);
                break;
            case ButtonType.Load:
                UILogic.instance.LoadSlot(buttonIndex);
                break;
            default:
                break;
        }
    }
}
