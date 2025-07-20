using UnityEngine;
using UnityEngine.AI;

public class ArenaSetup : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Camera _camera;

    private IControllable _playerControllable;
    private IControllable _agentAIControllable;

    private Controller _playerController;
    private Controller _agentAIController;
    private Controller _currentController;

    private PointerVisualizer _pointer;
    private NavMeshQueryFilter _queryFilter;

    private float _time;
    private float _timeToChangeController = 3f;

    private int _leftMouseButton = 0;

    private void Awake()
    {
        _pointer = GetComponent<PointerVisualizer>();

        _playerControllable = new PlayerControl(_character, _pointer, _layerMask, _camera);
        _playerController = _playerControllable.Initialize();

        _queryFilter = new NavMeshQueryFilter();
        _queryFilter.agentTypeID = 0;
        _queryFilter.areaMask = NavMesh.AllAreas;

        _agentAIControllable = new AgentAIControl(_character, _queryFilter);
        _agentAIController = _agentAIControllable.Initialize();

        _currentController = _playerController;
        _currentController.Enable();
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_character.HealthPoints <= 0)
        {
            _currentController.Disable();
            _character.StopMove();
        }

        if (_time >= _timeToChangeController)
        {
            _currentController.Disable();
            _currentController = _agentAIController;
            _currentController.Enable();
        }

        if (Input.GetMouseButtonDown(_leftMouseButton) && _time >= _timeToChangeController)
        {
            _time = 0;

            _currentController.Disable();
            _currentController = _playerController;
            _currentController.Enable();
        }

        _currentController.Update(Time.deltaTime);
    }
}