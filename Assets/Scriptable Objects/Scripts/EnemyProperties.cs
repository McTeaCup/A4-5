using UnityEngine;

public enum MovementType
{
    SideToSide,
    WShape,
    HourGlass,
    Circle,
    Swarm
}

public enum ApprochStyle
{
    Standard,
    Lerp,
    Slerp,
    Wave
}

[CreateAssetMenu(menuName = "Enemy", fileName = "Enemy Properties")]
public class EnemyProperties : ScriptableObject
{
    [Header("Health")]
    public float health;
    public int pointValue = 100;

    [Header("Movement")]
    public MovementType movementType;
    public ApprochStyle aprochStyle;
    public float movementSpeed;

    [Header("Wave Parameters")]
    [Range(0.5f, 1f)] public float hight = 1f;
    [Range(1f, 20f)] public float frequency = 5f;

    [Header("Weapon")]
    public float damage = 10f;
    public float fireRate = 3f;
    [Range(0, 360)]public float bulletAngle = 120f;
    [Range(0, 10)]public int bulletAmount = 5;

    [Header("Placeholder Stuff")]
    public Color spriteColor;
}
