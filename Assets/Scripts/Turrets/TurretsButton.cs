using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class TurretsButton : MonoBehaviour
{
    public Turrets_Info turret;
    public TextMeshProUGUI damageTxt;
    public TextMeshProUGUI fireRateTxt;
    public TextMeshProUGUI RangeTxt;
    public GameObject image;


    // Start is called before the first frame update
    void Start()
    {
        LoadInfo();
    }


    private void LoadInfo()
    {
        damageTxt.text = "Damage : " + turret.damage.ToString();
        fireRateTxt.text = "Fire Rate : " + turret.fireRate.ToString();
        RangeTxt.text = "Range : " + turret.Range.ToString();

        image.GetComponent<UnityEngine.UI.Image>().sprite = turret.model;
    }
    public Turrets_Info GetTurretInfo => turret;
}
