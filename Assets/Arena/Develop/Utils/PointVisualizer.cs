using UnityEngine;

public class PointVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject _pointer;

    private GameObject _currentPoint;

    public void VisualizePoint(Vector3 position)
    {
        if (_currentPoint == null)
            _currentPoint = Instantiate(_pointer, position, Quaternion.identity);
        else
        {
            Destroy(_currentPoint);
            _currentPoint = Instantiate(_pointer, position, Quaternion.identity);
        }
    }
}