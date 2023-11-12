using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public enum ButtonType
    {
        Save,
        Load,
        SceneChange
    }
    public ButtonType type;

    [Header("Save System settings")]
    public int buttonIndex;
    [Header("Scene Change settings")]
    public string sceneName;

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
            case ButtonType.SceneChange:
                UILogic.instance.LoadScene(sceneName);
                break;
            default:
                break;
        }
    }
}
