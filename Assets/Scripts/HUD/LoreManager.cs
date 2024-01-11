using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoreManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Lore_ScriptableObject loreScriptable;
    [SerializeField] private TextMeshProUGUI loreTextToDisplay;
    [SerializeField] private GameObject loreCanvas;
    [SerializeField] private EnemiesSpawnManager enemiesSpawnManager;

    [Header("Varibales")]
    private int _textNumber;
    public float loreSpeed;
    private string loreFullText;
    private string loreActualText;
    private List<string> loreList;

    private void Awake()
    {
        LoreManagerInitialization();
    }

    private void Start()
    {
        
    }

    public void NextLoreBtn()
    {
        if (loreList[_textNumber + 1].Length > 1)
        {
            _textNumber++;
            loreActualText = "";
            loreFullText = loreList[_textNumber];
            StartCoroutine(LoreDisplay());
        }
        else
        {
            loreCanvas.SetActive(false);
            //enemiesSpawnManager.startSpawning = true;
        }
    }

    private IEnumerator LoreDisplay()
    {
        for (int i = 0; i < loreFullText.Length + 1; i++)
        {
            loreActualText = loreFullText.Substring(0, i);
            loreTextToDisplay.text = loreActualText;
            yield return new WaitForSeconds(loreSpeed);
        }
    }

    private void LoreManagerInitialization()
    {
        _textNumber = 0;
        loreSpeed = 0.05f;
        loreActualText = "";
        loreList = new List<string>() { loreScriptable.text1, loreScriptable.text2, loreScriptable.text3, loreScriptable.text4, loreScriptable.text5 };
        loreFullText = loreList[_textNumber];
        StartCoroutine(LoreDisplay());
    }
}
