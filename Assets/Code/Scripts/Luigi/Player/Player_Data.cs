using UnityEngine;

[CreateAssetMenu(fileName = "Player_Data", menuName = "Character/Player_Data")]
public class Player_Data : Character_Data
{
    public float jumpForce = 5f;

    [Header("Rotation Settings")]
    public float sensibility = 1f;
}
