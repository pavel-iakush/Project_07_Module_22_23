using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDirectionalMovable, IDirectionalRotatable
{
    private NavMeshAgent _agent;

    private AgentMover _mover;
    private DirectionalRotator _rotator;
    private HealthPoints _health;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private int _healthPoints;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public Quaternion CurrentRotation => _rotator.CurrentRotation;

    public Vector3 Position => _agent.nextPosition;

    public Vector3 Destination => _agent.destination;

    public int HealthPoints => _health.Value;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        _health = new HealthPoints(_healthPoints);
    }

    private void Update()
    {
        _rotator.Update(Time.deltaTime);
    }
    
    public void SetMoveDirection(Vector3 position) => _mover.SetDestination(position);

    public void StopMove() => _mover.Stop();

    public void SetRotationDirection(Vector3 inputDirection) => _rotator.SetInputDirection(inputDirection);

    public void TakeDamage(int damage) => _health.TakeDamage(damage);
}