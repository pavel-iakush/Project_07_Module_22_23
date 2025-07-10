using UnityEngine;

public class FloatingAnimation : MonoBehaviour
{
    private float _time;

    private float _rotateSpeed;
    private float _minimumRotationValue = 200.0f;
    private float _maximumRotationValue = 360.0f;

    private float _timeScale;
    private float _minimumTimeValue = 8.0f;
    private float _maximumTimeValue = 14.0f;

    private float _yFactor = 500.0f;

    private void Awake()
    {
        _rotateSpeed = Random.Range(_minimumRotationValue, _maximumRotationValue);
        _timeScale = Random.Range(_minimumTimeValue, _maximumTimeValue);
    }

    private void Update()
    {
        _time += Time.deltaTime;

        transform.Rotate(Vector3.up, _rotateSpeed * Time.deltaTime);
        transform.position += Vector3.up * Mathf.Sin(_time * _timeScale) / _yFactor;
    }
}