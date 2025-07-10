using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    private NavMeshAgent _agent;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => _agent.destination;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }

    public void SetDestination(Vector3 position) => _mover.SetDestination(position);

    public void StopMove() => _mover.Stop();

    public void ResumeMove() => _mover.Resume();

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public bool TryGetPath(Vector3 targetPosition, NavMeshPath pathToTarget) => NavMeshUtils.TryGetPath(_agent, targetPosition, pathToTarget);

    public void SetMoveDirection(Vector3 inputDirection)
    {
        throw new System.NotImplementedException();
    }
}