using UnityEngine;

/// <summary>
/// A gomb logikáját kezelõ osztály.
/// </summary>
public class ButtonLogic : MonoBehaviour
{
    /// <summary>
    /// Az elérhetõ gombtípusok felsorolása.
    /// </summary>
    public enum ButtonType
    {
        Resume,
        Save,
        Load,
        SceneChange,
        Difficulty
    }

    /// <summary>
    /// A gomb típusa.
    /// </summary>
    public ButtonType type;

    /// <summary>
    /// Mentési rendszer beállításai.
    /// </summary>
    [Header("Save System settings")]
    public int buttonIndex;

    /// <summary>
    /// Jelenetváltás beállításai.
    /// </summary>
    [Header("Scene Change settings")]
    public string sceneName;

    /// <summary>
    /// Nehézségi beállítások.
    /// </summary>
    [Header("Difficulty settings")]
    public int scoreGoal;
    public string difficulty;

    /// <summary>
    /// A gomb lenyomásakor végrehajtandó mûveletek.
    /// </summary>
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
