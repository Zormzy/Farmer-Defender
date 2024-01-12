using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private AudioManager _audioManager;

    public void OnESCPauseBtn(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!_pauseCanvas.activeSelf)
            {
                _audioManager.gamePaused = true;
                _pauseCanvas.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                _audioManager.gamePaused = false;
                _pauseCanvas.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void OnPauseBtn()
    {
        _audioManager.gamePaused = true;
        _pauseCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void OnReturnBtn()
    {
        _audioManager.gamePaused = false;
        _pauseCanvas.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnMainMenuBtn()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
