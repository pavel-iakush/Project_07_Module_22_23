using UnityEngine;
using UnityEngine.AI;

public class ControlSwitcher/* : MonoBehaviour*/
{
    /*[SerializeField] */private AgentCharacter _character;
    /*[SerializeField] */private LayerMask _layerMask;
    /*[SerializeField] */private Camera _camera;

    private RaycastService _raycastService;
    private NavMeshQueryFilter _queryFilter;

    private Controller _playerController;
    private Controller _agentAIController;
    private Controller _currentController;

    private Controller _rotationController;

    private float _time;
    private float _timeToChangeController = 2f;
    private int _leftMouseButton = 0;

    public ControlSwitcher(AgentCharacter character, LayerMask layerMask, Camera camera)
    {
        _character = character;
        _layerMask = layerMask;
        _camera = camera;
    }

    public Controller CurrentController => _currentController;

    public Controller RotationController => _rotationController;

    public void Awake()
    {
        _raycastService = new RaycastService();

        _rotationController = new AlongMovableVelocityRotatableController(_character, _character);

        _queryFilter = new NavMeshQueryFilter();
        _queryFilter.agentTypeID = 0;
        _queryFilter.areaMask = NavMesh.AllAreas;

        _playerController = new ClickToMoveAgentController(_character, _layerMask, _raycastService, _camera);
        _agentAIController = new AreaPatrolAgentController(_character, _queryFilter);

        _currentController = _playerController;
        _currentController.Enable();
    }

    public void Update()
    {
        _time += Time.deltaTime;

        if (_character.HealthPoints <= 0)
        {
            ShutdownAgentMovement();
            return;
        }

        if (Input.GetMouseButtonDown(_leftMouseButton))
        {
            _time = 0;

            if (_currentController == _agentAIController)
            {
                SwitchController(_playerController);
            }
        }

        if (IsPlayerAFK())
        {
            SwitchController(_agentAIController);
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

    private void SwitchController(Controller newController)
    {
        if (_currentController == newController)
            return;

        _currentController.Disable();
        _currentController = newController;
        _currentController.Enable();
    }
}