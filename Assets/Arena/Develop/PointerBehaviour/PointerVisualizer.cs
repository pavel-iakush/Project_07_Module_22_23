using UnityEngine;

public class PointerVisualizer : MonoBehaviour
{
    [SerializeField] private Pointer _pointer;
    [SerializeField] private AgentCharacter _character;

    private GameObject _currentPointer;
    private float _deadZone = 0.1f;

    private void Update()
    {
        if (_currentPointer == null)
            return;

        if (IsPointerReached())
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
        => _currentPointer = Instantiate(_pointer.gameObject, position, Quaternion.identity);

    private void ClearPointer()
        => Destroy(_currentPointer);

    private bool IsPointerReached()
        => (_character.Position - _currentPointer.transform.position).magnitude < _deadZone;
}