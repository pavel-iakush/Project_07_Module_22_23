using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BombBehaviour : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;

    [SerializeField] private ParticleSystem _alarmEffect;
    [SerializeField] private ParticleSystem _explosionEffect;

    private int _explosionValue = 30;

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
        if ((_character.Position - transform.position).magnitude < _activateRadius)
        {
            _time += Time.deltaTime;

            _emission.rateOverTime = _activeRate;

            _color.color = Color.red;

            if (_time >= _timeToExplode)
            {
                ParticleSystem currentExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
                currentExplosion.gameObject.SetActive(true);
                currentExplosion.Play();

                Destroy(gameObject);

                _character.TakeDamage(_explosionValue);
            }
        }
        else
        {
            _time = 0;

            _emission.rateOverTime = _defaultRate;

            _color.color = _defaultColor;
        }
    }
}