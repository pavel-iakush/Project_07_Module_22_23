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

    private NavMeshQueryFilter _queryFilter;

    private float _time;
    private float _timeToChangeController = 3f;

    private int _leftMouseButton = 0;

    private void Awake()
    {
        _playerControllable = new PlayerControl(_character, _layerMask, _camera);
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
            ShutdownAgentMovement();
        }

        if (_time >= _timeToChangeController)
        {
            SwitchToAgentAIControl();
        }

        if (IsMousePressedWhileControlledByAI())
        {
            _time = 0;

            SwitchToPlayerControl();
        }

        _currentController.Update(Time.deltaTime);
    }

    private void ShutdownAgentMovement()
    {
        _currentController.Disable();
        _character.StopMove();
    }

    private bool IsMousePressedWhileControlledByAI()
    {
        return Input.GetMouseButtonDown(_leftMouseButton) && _time >= _timeToChangeController;
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