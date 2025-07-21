using UnityEngine;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    private BombTimer _timer;

    private float _activateRadius = 3f;
    private float _timeToExplode = 2.5f;

    private int _explosionValue = 40;

    private bool _hasExploded;

    public BombTimer Timer => _timer;

    private void Awake()
    {
        _timer = new BombTimer(_timeToExplode);
    }

    private void Update()
    {
        if (IsCharacterInRange())
        {
            Activate();

            if (_timer.IsOutOfTime())
                Explode();
        }
        else
        {
            Deactivate();
        }
    }

    private void Deactivate() => _timer.Reset();

    private void Activate() => _timer.Start(Time.deltaTime);

    private void Explode()
    {
        if (_hasExploded)
            return;

        _hasExploded = true;

        _timer.TriggerExplosion();
        _character.TakeDamage(_explosionValue);

        Destroy(gameObject, 0.1f);
    }

    private bool IsCharacterInRange()
    {
        return (_character.Position - transform.position).magnitude <= _activateRadius;
    }
}