using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Economie : MonoBehaviour
{
    public static Economie Instance;
    public TextMeshProUGUI GoldTxt;
    public Player_ScriptableObject player;

    public int numberOfGold;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) { Instance = this; }
        numberOfGold = player.moneyStart;
        ChangeTxt();
    }

    private void ChangeTxt()
    {
        GoldTxt.text = "Gold : " + numberOfGold.ToString();
    }

    public void AddGolds(int gold)
    {
        numberOfGold += gold;
        ChangeTxt();
    }

    public bool BuySomething(int cost)
    {
        bool canbuy = CanBuy(cost);
        if (canbuy)
            numberOfGold -= cost;
        ChangeTxt();
        return canbuy;
    }

    public bool CanBuy(int cost)
    {
        if (cost > numberOfGold)
        {
            return false;
        }
        return true;
    }
}
