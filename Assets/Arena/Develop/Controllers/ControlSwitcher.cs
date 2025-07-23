using UnityEngine;
using UnityEngine.AI;

public class ControlSwitcher : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Camera _camera;

    private RaycastService _raycastService;
    private NavMeshQueryFilter _queryFilter;

    private Controller _playerController;
    private Controller _agentAIController;
    private Controller _currentController;

    private float _time;
    private float _timeToChangeController = 3f;
    private int _leftMouseButton = 0;

    public Controller CurrentController => _currentController;

    private void Awake()
    {
        _raycastService = new RaycastService();

        _queryFilter = new NavMeshQueryFilter();
        _queryFilter.agentTypeID = 0;
        _queryFilter.areaMask = NavMesh.AllAreas;

        _playerController = new ClickToMoveAgentController(_character, _layerMask, _raycastService, _camera);
        _agentAIController = new AreaPatrolAgentController(_character, _queryFilter);

        _currentController = _playerController;
        _currentController.Enable();
    }

    private void Update()
    {
        _time += Time.deltaTime;

        if (_character.HealthPoints <= 0)
        {
            ShutdownAgentMovement();
            return;
        }

        if (IsPlayerAFK())
        {
            SwitchToAgentAIControl();
        }

        if (IsMousePressedWhileControlledByAI())
        {
            _time = 0;
            SwitchToPlayerControl();
        }
    }

    private void ShutdownAgentMovement()
    {
        _currentController.Disable();
        _character.StopMove();
    }

    private bool IsPlayerAFK()
    {
        return _time >= _timeToChangeController && _currentController == _playerController;
    }

    private bool IsMousePressedWhileControlledByAI()
    {
        return Input.GetMouseButtonDown(_leftMouseButton) && _currentController == _agentAIController;
    }

    private void SwitchToPlayerControl()
    {
        _currentController.Disable();
        _currentController = _playerController;
        _currentController.Enable();
    }

    private void SwitchToAgentAIControl()
    {
        _currentController.Disable();
        _currentController = _agentAIController;
        _currentController.Enable();
    }
}