using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    START_GAME,
    SELECT_LEVEL,
    PAUSE_GAME,
    RESUME_GAME,
    RESTART_LEVEL,
    MAIN_MENU,
    LEVEL
}

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public ButtonType buttonType;

    CanvasManager canvasManager;
    Button menuButton;
    Game game;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.GetInstance();
        game = Game.GetInstance();
    }

    void OnButtonClicked()
    {
        switch (buttonType)
        {
            case ButtonType.START_GAME:
                game.LoadNextLevel(); // load first/next level
                canvasManager.SwitchCanvas(CanvasType.GameUI);
                break;
            case ButtonType.SELECT_LEVEL:
                canvasManager.SwitchCanvas(CanvasType.LevelSelector);
                break;
            case ButtonType.PAUSE_GAME:
                game.PauseGame();
                canvasManager.SwitchCanvas(CanvasType.PauseMenu);
                break;
            case ButtonType.RESUME_GAME:
                game.ResumeGame();
                canvasManager.SwitchCanvas(CanvasType.GameUI);
                break;
            case ButtonType.RESTART_LEVEL:
                game.RestartLevel();
                canvasManager.SwitchCanvas(CanvasType.GameUI);
                break;
            case ButtonType.MAIN_MENU:
                game.UnloadLevel();
                canvasManager.SwitchCanvas(CanvasType.MainMenu);
                break;
            case ButtonType.LEVEL:
                canvasManager.SwitchCanvas(CanvasType.GameUI);
                break;
            default:
                break;
        }
    }
}
