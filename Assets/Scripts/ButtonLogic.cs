using UnityEngine;

/// <summary>
/// A gomb logik�j�t kezel� oszt�ly.
/// </summary>
public class ButtonLogic : MonoBehaviour
{
    /// <summary>
    /// Az el�rhet� gombt�pusok felsorol�sa.
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
    /// A gomb t�pusa.
    /// </summary>
    public ButtonType type;

    /// <summary>
    /// Ment�si rendszer be�ll�t�sai.
    /// </summary>
    [Header("Save System settings")]
    public int buttonIndex;

    /// <summary>
    /// Jelenetv�lt�s be�ll�t�sai.
    /// </summary>
    [Header("Scene Change settings")]
    public string sceneName;

    /// <summary>
    /// Neh�zs�gi be�ll�t�sok.
    /// </summary>
    [Header("Difficulty settings")]
    public int scoreGoal;
    public string difficulty;

    /// <summary>
    /// A gomb lenyom�sakor v�grehajtand� m�veletek.
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
