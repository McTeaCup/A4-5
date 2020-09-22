using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    PlayerInput _input;
    [Header("Ability Prefabs")]
    public GameObject[] _abilities;

    [Header("Ability Statements")]
    public bool abilityActive = false;
    [SerializeField] float _abilityActivationCooldown = default;
    float _nextTimeToActivation;
    IAbility currentAbility = default;

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _nextTimeToActivation = _abilityActivationCooldown;
    }

    private void Update()
    {
        //If ability is on cooldown
        if (!abilityActive && _nextTimeToActivation <= _abilityActivationCooldown)
        {
            _nextTimeToActivation += Time.deltaTime;
        }

        //If player activate ability
        if (_nextTimeToActivation > _abilityActivationCooldown && _input.AbilityActivation() > 0)
        {
            abilityActive = true;
            _nextTimeToActivation = 0;

            switch (_input.AbilityActivation())
            {
                case 1:
                    {
                        if (currentAbility == null)
                        {
                            Instantiate(_abilities[0], transform);
                        }

                        break;
                    }
            }
        }
    }
}