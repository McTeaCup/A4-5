using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName ="Movement")]
public class PlayerProperties : ScriptableObject
{
    public float health;
    public float movementSpeed;
    public float fireRate;
}
