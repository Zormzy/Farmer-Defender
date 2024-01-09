using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Turret")]
public class Turrets_Info : ScriptableObject
{
    public float damage;
    public float Range;
    public float fireRate;
    public Sprite model;
    public GameObject turret;
}
