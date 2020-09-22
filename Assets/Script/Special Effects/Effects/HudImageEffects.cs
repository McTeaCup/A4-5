using System.Threading;
using UnityEngine;

public class HudImageEffects : MonoBehaviour
{
    [Header("Position")]
    [Tooltip("Float 1 = X-value\nFloat 2 = Y-value")]
    [Range(-10000f, 10000f)] [SerializeField] float[] _offset = new float[2];
    [SerializeField] Transform _midOfScreen = default;
    
    [Header("Speed & Duration")]
    [Range(0.1f, 10f)][SerializeField] float _speed = 5f;
    [SerializeField] float _activeTime = 0f;
    float _timer = 0f;
    int _status = 0;
    float _effectIndex;

    private void Update()
    {
        switch (_effectIndex) //Triggers effect (depending on what is activated)
        {
            case 0:
                {
                    _timer -= Time.deltaTime;
                    if (_status == 0)
                    {
                        MoveObjectInPlace();
                    }

                    else if (_status == 1)
                    {
                        MoveObjectOutOfPlace();
                    }
                    break;
                }
        }
    }

    public void Slide() //Sets image on offset's position
    {
        transform.position = new Vector3(_midOfScreen.position.x + _offset[0], _midOfScreen.position.y + _offset[1]);
        _effectIndex = 0;
        _status = 0;
        _timer = _activeTime;
    }

    void MoveObjectInPlace()    //Moves image to middle of screen
    {
        transform.position = Vector3.Lerp(transform.position, _midOfScreen.position,
            Time.deltaTime * _speed);

        if (Vector3.Distance(transform.position, _midOfScreen.position) < 0.1f)
        {
            if (_timer < 0.5f)
            {
                _status = 1;
            }
        }
    }

    void MoveObjectOutOfPlace() //Moves the image away from middle screen
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(_midOfScreen.position.x + -_offset[0], _midOfScreen.position.y + -_offset[1]),
            Time.deltaTime * _speed);
    }
}
