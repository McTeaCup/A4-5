using UnityEngine;
using UnityEngine.UI;

public class PointManger : MonoBehaviour
{
    [HideInInspector] public float totalPoints = 0;
    [HideInInspector] public float drainSpeed = 50;

    [Header("Information")]
    [SerializeField] WeaponFunction _player = default;
    [SerializeField] HUDManager _playerHUD = default;
    [SerializeField] EffectManager _effects = default;
    [SerializeField] Text _finalPoint = default;

    [SerializeField] int _bulletAmount = 0;

    [Header("Multiplier")]
    [SerializeField] [Range(1, 1000)] float currentMultiplierThreshold = default;
    [SerializeField] float _multiplierPoints = 0;
    [SerializeField] float _highestThreshold = default;

    private void Awake()
    {
        _playerHUD.xpSlider.minValue = 0;
        _highestThreshold = currentMultiplierThreshold;
        UpdateHUD();
    }

    void Update()
    {
        UpdateXPThreshold();

        UpdateHUD();

        _player.BulletMultiplier(_bulletAmount);
    }

    public void PlayerPointsUpdate(float newPoint) //Update player points
    {
        _effects.events[0].Invoke();                              //Trigger totalpoint effects
        _multiplierPoints += (newPoint * 0.5f);
        totalPoints += newPoint;
    }

    void UpdateXPThreshold()    //Sets new XP threshold
    {
        if (_multiplierPoints >= currentMultiplierThreshold)
        {
            _effects.events[1].Invoke();        //Trigger grow effect
            currentMultiplierThreshold *= 5;    //Sets new threshold to be 5* the last level's threshold
            _highestThreshold = currentMultiplierThreshold;
            _bulletAmount++; //Increeses total amount of bullets when fired
        }
    }

    void UpdateHUD()    //Updates values
    {
        _finalPoint.text = totalPoints.ToString();
        _playerHUD.multiplier.text = $"{_bulletAmount + 1}x";
        _playerHUD.totalPoints.text = $"{totalPoints} Points";
        _playerHUD.xpSlider.maxValue = currentMultiplierThreshold;
        _playerHUD.xpSlider.value = _multiplierPoints;
    }
}
