using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Vector3 playerFinalInput = new Vector3(0f, 0f, 0f);
    int step = 0;
    int abilityIndex = 0;

    private void Update()
    {
        MovementInput();
    }

    public Vector3 MovementInput()
    {
        Vector3 _playerMovementKeyInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        Vector3 _playerMovementMouseInput = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0f);

        playerFinalInput = new Vector3(_playerMovementKeyInput.x + _playerMovementMouseInput.x, _playerMovementKeyInput.y + _playerMovementMouseInput.y);
        return playerFinalInput;
    }

    public bool WeaponFire()
    {
        //Normal Fire
        if (Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.Mouse0))
        {
            return true;
        }

        return false;
    }

    public int AbilityActivation()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            abilityIndex = 1;
            return abilityIndex = 1;
            ;
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilityIndex = 2;
            return abilityIndex;
        }

        else
        {
            return 0;
        }
    }

    public int WeaponSwitch()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            step = 1;
        }

        else if (Input.GetKeyDown(KeyCode.Q))
        {
            step = -1;
        }

        else
        {
            step = 0;
        }

        return step;
    }
}
