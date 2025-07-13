using UnityEngine;

public class AgentCharacterView : MonoBehaviour
{
    private readonly int IsWalkingKey = Animator.StringToHash("IsWalking");

    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;

    private int _woundedLayer = 1;
    private float _woundedWeight = 1.0f;

    private float _standValue = 0.05f;

    private int _cachedHealth;

    private void Start()
    {
        _cachedHealth = _character.HealthPoints;
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

        if (_character.HealthPoints <= 30)
            _animator.SetLayerWeight(_woundedLayer, _woundedWeight);

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
        _animator.SetTrigger("HitTrigger");
    }

    private void Death()
    {
        _animator.SetTrigger("IsDeadTrigger");
    }
}