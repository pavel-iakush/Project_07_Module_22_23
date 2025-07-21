using UnityEngine;

public class PointerVisualizer : MonoBehaviour
{
    [SerializeField] private Pointer _pointerPrefab;
    [SerializeField] private AgentCharacter _character;

    private GameObject _currentPointer;
    private float _deadZone = 0.1f;

    private void Update()
    {
        if (HasClickedSomewhere())
            VisualizePoint(_character.Destination);

        if (_currentPointer != null && IsPointerReached())
            ClearPointer();
    }

    public void VisualizePoint(Vector3 position)
    {
        if (_currentPointer == null)
            CreateNewPointer(position);
        else
        {
            ClearPointer();
            CreateNewPointer(position);
        }
    }

    private void CreateNewPointer(Vector3 position)
        => _currentPointer = Instantiate(_pointerPrefab.gameObject, position, Quaternion.identity);

    private void ClearPointer()
    {
        if (_currentPointer != null)
        {
            Destroy(_currentPointer);
            _currentPointer = null;
        }
    }

    private bool HasClickedSomewhere()
        => (_character.Position - _character.Destination).magnitude > _deadZone;

    private bool IsPointerReached()
        => (_character.Position - _currentPointer.transform.position).magnitude <= _deadZone;
}