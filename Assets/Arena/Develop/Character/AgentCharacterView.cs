using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsWalkingKey = Animator.StringToHash("IsWalking");
    private readonly int HitKey = Animator.StringToHash("HitTrigger");
    private readonly int DeathKey = Animator.StringToHash("IsDeadTrigger");

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private int _woundedLayer = 1;
    private float _layerFullWeight = 1.0f;
    private float _woundedValue = 0.3f;

    private float _standLimit = 0.05f;

    private int _cachedHealth;
    private int _startHealth;

    private void Start()
    {
        _cachedHealth = _character.HealthPoints;
        _startHealth = _character.HealthPoints;
    }

    private void Update()
    {
        if (_character.CurrentVelocity.magnitude > _standLimit)
            StartRunning();
        else
            StopRunning();

        if (_character.HealthPoints < _cachedHealth)
        {
            PlayHitAnimation();
            _cachedHealth = _character.HealthPoints;
        }

        if (_character.HealthPoints <= _startHealth * _woundedValue)
            ActivateWoundedState();

        if (_character.HealthPoints <= 0)
            PlayDeathAnimation();
    }

    private void StopRunning()
    {
        _animator.SetBool(IsWalkingKey, false);
    }

    private void StartRunning()
    {
        _animator.SetBool(IsWalkingKey, true);
    }

    private void PlayHitAnimation()
    {
        _animator.SetTrigger(HitKey);
    }

    private void ActivateWoundedState()
    {
        _animator.SetLayerWeight(_woundedLayer, _layerFullWeight);
    }

    private void PlayDeathAnimation()
    {
        _animator.SetTrigger(DeathKey);
    }
}