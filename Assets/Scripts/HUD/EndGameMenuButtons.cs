using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenuButtons : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private PlayerManager _playerManager;

    [Header("Variables")]
    private string LevelToReload = "";
    private int levelNumberToReload = 0;

    public void OnMainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnRetryBtn()
    {
        levelNumberToReload = _playerManager._playerActualLevel;
        LevelToReload = "Level_" + levelNumberToReload;
        SceneManager.LoadScene(LevelToReload);
    }
}
