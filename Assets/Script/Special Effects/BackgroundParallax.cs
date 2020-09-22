using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [Range(0, 1)][SerializeField]float _scrollSpeed = default;
    Vector2 startPosition = default;

    private void Awake()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        float newPosition = Mathf.Repeat(Time.time * _scrollSpeed, 10);
        transform.position = startPosition + -Vector2.up * newPosition;
    }

}
