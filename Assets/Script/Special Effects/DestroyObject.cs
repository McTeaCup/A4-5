using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] float _destroyObjectTimer = 2f;

    private void Awake()
    {
        Destroy(gameObject, _destroyObjectTimer);
    }
}
