using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScritpable", menuName = "ScriptableObjects/Enemy_Scritpable")]
public class Enemy_ScriptableObject : ScriptableObject
{
    public string enemyName;
    public int enemyMaxHP;
    public int enemyHP;
    public int enemyDamage;
    public float enemySpeed;
    public int gold;
}
