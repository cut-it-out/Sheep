using UnityEngine;
using UnityEngine.UI;

public enum ButtonType
{
    START_GAME,
    SELECT_LEVEL,
    PAUSE_GAME,
    RESUME_GAME,
    RESTART_GAME
}

[RequireComponent(typeof(Button))]
public class ButtonController : MonoBehaviour
{
    public ButtonType buttonType;

    CanvasManager canvasManager;
    Button menuButton;

    private void Start()
    {
        menuButton = GetComponent<Button>();
        menuButton.onClick.AddListener(OnButtonClicked);
        canvasManager = CanvasManager.GetInstance();
    }

    void OnButtonClicked()
    {
        switch (buttonType)
        {
            case ButtonType.START_GAME:
                //Call other code that is necessary to start the game like levelManager.StartGame()
                canvasManager.SwitchCanvas(CanvasType.GameUI);
                break;
            case ButtonType.SELECT_LEVEL:
                canvasManager.SwitchCanvas(CanvasType.LevelSelector);
                break;
            case ButtonType.PAUSE_GAME:
                canvasManager.SwitchCanvas(CanvasType.PauseMenu);
                break;
            case ButtonType.RESUME_GAME:
                canvasManager.SwitchCanvas(CanvasType.GameUI);
                break;
            case ButtonType.RESTART_GAME:
                canvasManager.SwitchCanvas(CanvasType.GameUI);
                break;
            default:
                break;
        }
    }
}
