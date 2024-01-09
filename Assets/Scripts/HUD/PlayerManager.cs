using TMPro;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Player_ScriptableObject _playerScriptableObject;
    [SerializeField] private TextMeshProUGUI _playerHPText;
    [SerializeField] private TextMeshProUGUI _victoryStatusText;
    [SerializeField] private GameObject _victoryCanvas;

    [Header("Variables")]
    private int _playerMaxHP;
    private int _playerHP;
    public int _playerActualLevel;

    private void Awake()
    {
        PlayerManagerInitialization();
    }

    public void OnPlayerTakeDamage(int damage)
    {
        if (_playerHP - damage <= 0)
        {
            _victoryStatusText.text = "Failed";
            _victoryCanvas.SetActive(true);
        }
        else
        {
            _playerHP -= damage;
            _playerHPText.text = "Player HP : \n" + _playerHP;
        }
    }

    private void PlayerManagerInitialization()
    {
        _playerMaxHP = _playerScriptableObject.playerMaxHP;
        _playerHP = _playerMaxHP;
        _playerActualLevel = _playerScriptableObject.levelNumber;
        _playerHPText.text = "Player HP : \n" + _playerHP;
    }
}