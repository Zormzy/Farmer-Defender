using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScritpable", menuName = "ScriptableObjects/Player_Scritpable")]
public class Player_ScriptableObject : ScriptableObject
{
    public int levelNumber;
    public int playerMaxHP;
    public int playerHP;
}
