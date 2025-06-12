using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/Character Data")]
public class Character_Data : ScriptableObject
{
    public string characterName;

    [Header("Stats")]
    public float moveSpeed = 5f;
    public int maxHealth = 100;
    public int damage = 10;
}
