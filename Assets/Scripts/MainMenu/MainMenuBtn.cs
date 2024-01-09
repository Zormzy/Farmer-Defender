using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBtn : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject _playButton;
    [SerializeField] private GameObject _creditsButton;
    [SerializeField] private GameObject _quitButton;
    [SerializeField] private GameObject _returnButton;
    [SerializeField] private GameObject _creditsCanvas;

    public void OnPlayBtn()
    {
        SceneManager.LoadScene("Level_1");
    }

    public void OnCreditsBtn()
    {
        _playButton.SetActive(false);
        _creditsButton.SetActive(false);
        _quitButton.SetActive(false);
        _returnButton.SetActive(true);
        _creditsCanvas.SetActive(true);
    }

    public void OnReturnBtn()
    {
        _playButton.SetActive(true);
        _creditsButton.SetActive(true);
        _quitButton.SetActive(true);
        _returnButton.SetActive(false);
        _creditsCanvas.SetActive(false);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }
}
