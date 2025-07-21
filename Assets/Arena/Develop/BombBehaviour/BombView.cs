using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BombView : MonoBehaviour
{
    [SerializeField] private ParticleSystem _alarmEffect;
    [SerializeField] private ParticleSystem _explosionEffect;

    private BombBehaviour _bombBehaviour;
    private bool _hasPlayedExplosion;

    private float _defaultRate = 2f;
    private float _activeRate = 5.5f;
    private Color _defaultColor = Color.white;

    private EmissionModule _emission;
    private ColorOverLifetimeModule _color;

    private void Start()
    {
        _bombBehaviour = GetComponentInParent<BombBehaviour>();

        _emission = _alarmEffect.emission;
        _color = _alarmEffect.colorOverLifetime;
    }

    void Update()
    {
        if (_bombBehaviour.Timer.IsActive)
            ActiveState();
        else
            IdleState();

        if (_bombBehaviour.Timer.IsExplode && _hasPlayedExplosion != true)
        {
            PlayExplosionEffect();
            _hasPlayedExplosion = true;
        }
    }

    private void ActiveState()
    {
        _emission.rateOverTime = _activeRate;
        _color.color = Color.red;
    }

    private void IdleState()
    {
        _emission.rateOverTime = _defaultRate;
        _color.color = _defaultColor;
    }

    private void PlayExplosionEffect()
    {
        ParticleSystem currentExplosion = Instantiate(_explosionEffect, transform.position, Quaternion.identity);
        currentExplosion.gameObject.SetActive(true);
        currentExplosion.Play();
    }
}