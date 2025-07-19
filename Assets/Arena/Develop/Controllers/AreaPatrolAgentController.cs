using UnityEngine;
using UnityEngine.AI;

public class AreaPatrolAgentController : Controller
{
    private IDirectionalMovable _movable;

    private Vector3 _patrolPivot;
    private float _areaRange = 5f;

    private float _time;
    private float _timeToStartPatrol = 3f;

    private NavMeshQueryFilter _queryFilter;
    private NavMeshPath _pathToTarget = new NavMeshPath();

    public AreaPatrolAgentController(IDirectionalMovable movable, NavMeshQueryFilter queryFilter)
    {
        _movable = movable;
        _queryFilter = queryFilter;
    }

    protected override void UpdateLogic(float deltaTime)
    {
        _time += deltaTime;

        if (_time >= _timeToStartPatrol)
        {
            _patrolPivot = _movable.Position;

            Vector2 pointInCircle = Random.insideUnitCircle * _areaRange;
            Vector3 position = _patrolPivot + new Vector3(pointInCircle.x, 0, pointInCircle.y);

            if (NavMeshUtils.TryGetPath(_movable.Position, position, _queryFilter, _pathToTarget))
            {
                _movable.SetMoveDirection(position);
            }

            _time = 0;
        }
    }
}