using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    [SerializeField] private ParticleSystem _alarmEffect;
    [SerializeField] private ParticleSystem _explosionEffect;

    private int _explosionValue = 40;

    private float _time;
    private float _timeToExplode = 2.5f;

    private float _activateRadius = 3f;
    private float _activeRate = 5.5f;

    private float _defaultRate = 2f;
    private Color _defaultColor = Color.white;

    private EmissionModule _emission;
    private ColorOverLifetimeModule _color;

    private void Awake()
    {
        _emission = _alarmEffect.emission;

        _color = _alarmEffect.colorOverLifetime;
    }

    private void Update()
    {
        if (IsCharacterInRange())
        {
            ActivateBomb();

            if (_time >= _timeToExplode)
            {
                ExplodeBomb();
            }
        }
        else
        {
            DeactivateBomb();
        }
    }

    private void ExplodeBomb()
    {
        ParticleSystem currentExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        currentExplosion.gameObject.SetActive(true);
        currentExplosion.Play();

        Destroy(gameObject);

        _character.TakeDamage(_explosionValue);
    }

    private void DeactivateBomb()
    {
        _time = 0;

        _emission.rateOverTime = _defaultRate;
        _color.color = _defaultColor;
    }

    private void ActivateBomb()
    {
        _time += Time.deltaTime;

        _emission.rateOverTime = _activeRate;
        _color.color = Color.red;
    }

    private bool IsCharacterInRange()
    {
        return (_character.Position - transform.position).magnitude < _activateRadius;
    }
}