using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsWalkingKey = Animator.StringToHash("IsWalking");
    private const string HitKey = "HitTrigger";
    private const string DeathKey = "IsDeadTrigger";

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private int _woundedLayer = 1;
    private float _layerFullWeight = 1.0f;

    private float _standValue = 0.05f;

    private int _cachedHealth;
    private int _startHealth;

    private float _woundedValue = 0.3f;

    private void Start()
    {
        _cachedHealth = _character.HealthPoints;
        _startHealth = _character.HealthPoints;
    }

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > _standValue)
            StartRunning();
        else
            StopRunning();

        if (_character.HealthPoints < _cachedHealth)
        {
            Hit();
            _cachedHealth = _character.HealthPoints;
        }

        if (_character.HealthPoints <= _startHealth * _woundedValue)
            _animator.SetLayerWeight(_woundedLayer, _layerFullWeight);

        if (_character.HealthPoints <= 0)
            Death();
    }

    private void StopRunning()
    {
        _animator.SetBool(IsWalkingKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsWalkingKey, true);
    }

    private void Hit()
    {
        _animator.SetTrigger(HitKey);
    }

    private void Death()
    {
        _animator.SetTrigger(DeathKey);
    }
}