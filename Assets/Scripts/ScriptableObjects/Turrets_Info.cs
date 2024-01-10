using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Turret")]
public class Turrets_Info : ScriptableObject
{
    public int damage;
    public float Range;
    public float fireRate;
    public Sprite model;
    public GameObject turret;
    public int cost;
}
