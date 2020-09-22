using System.Security.Cryptography;
using UnityEngine;

public class TriggerEffect : MonoBehaviour
{
    [SerializeField] EffectManager _effects = default;

    void Start()
    {
        _effects.events[0].Invoke();
    }
}
