using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public WeaponFunction weapon;

    [Header("Item Components")]
    public Image itemIcon;
    public Text itemName;

    [Header("Score Components")]
    public Slider xpSlider;
    public Text currentWave;
    public Text totalPoints;
    public Text multiplier;

    public void UpdateHUD(Sprite _itemIcon, Color _itemColor, string _itemName)
    {
        itemIcon.sprite = _itemIcon;
        itemIcon.color = _itemColor;
        itemName.text = _itemName;
    }
}
