using UnityEngine;

public class PointView : MonoBehaviour
{
    private Vector3 _originalScale;

    private float _time;
    private float _amplitude = 0.001f;
    private float _frequency = 9.0f;
    private float _startYPosition;

    private float _scaleOffset = 1.1f;
    private float _offsetFactor = 0.05f;

    private void Awake()
    {
        _originalScale = transform.localScale;
        _startYPosition = transform.position.y;
    }

    private void Update()
    {
        _time += Time.deltaTime;

        float yPosition = _startYPosition + _amplitude * Mathf.Sin(_time * _frequency);
        transform.position = new Vector3(transform.position.x, yPosition, transform.position.z);

        transform.localScale = _originalScale * (_scaleOffset + (Mathf.Sin(_time * _frequency)) * _offsetFactor);
    }
}