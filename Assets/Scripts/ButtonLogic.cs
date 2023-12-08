using UnityEngine;

public class ButtonLogic : MonoBehaviour
{
    public enum ButtonType
    {
        Resume,
        Save,
        Load,
        SceneChange,
        Difficulty
    }
    public ButtonType type;

    [Header("Save System settings")]
    public int buttonIndex;
    [Header("Scene Change settings")]
    public string sceneName;
    [Header("Difficulty settings")]
    public int scoreGoal;
    public string difficulty;

    public void ButtonPressed()
    {
        switch (type)
        {
            case ButtonType.Resume:
                UILogic.instance.isPaused = false;
                UILogic.instance.ShowPauseMenu();
                break;
            case ButtonType.Save:
                UILogic.instance.SaveSlot(buttonIndex);
                break;
            case ButtonType.Load:
                UILogic.instance.LoadSlot(buttonIndex);
                break;
            case ButtonType.SceneChange:
                UILogic.instance.LoadScene(sceneName);
                break;
            case ButtonType.Difficulty:
                SaveSystem.instance.SaveGameParameters(scoreGoal);
                UILogic.instance.SetDifficultyText(difficulty);
                break;
            default:
                break;
        }
    }
}
