using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Weapon Name")]
public class WeaponProperties : ScriptableObject
{
    [Header("Standard Properties (These properties will apply to all bullets)")]
    public float damage = 10f;
    public float survivaltime = 3f;
    public float bulletSpeed = 1000f;
    public float fireSpeed = 0.2f;
    public float multiplierPoints = 10;

    [Header("Tracking Rockets")]
    public float rocketRadius = 2f;
    [Range(0.01f, 1f)] public float acceleration = 0.1f;

    [Header("Cosmetics")]
    public string itemName;
    [TextArea(2, 5)]public string weaponDiscription;
    public Sprite weaponImage;
    public Color bulletColor;
}
