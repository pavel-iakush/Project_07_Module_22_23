public class BombTimer
{
    private float _time;
    private float _timeToExplode;

    private bool _isActive;
    private bool _isExplode;

    public BombTimer(float timeToExplode)
    {
        _timeToExplode = timeToExplode;
    }

    public bool IsActive => _isActive;

    public bool IsExplode => _isExplode;

    public float Time
    {
        get
        {
            return _time;
        }
        set
        {
            if ( value < 0)
                _time = 0;

            _time = value;
        }
    }

    public void Start(float deltaTime)
    {
        _time += deltaTime;
        _isActive = true;
    }

    public void Reset()
    {
        _time = 0;
        _isActive = false;
    }

    public bool IsOutOfTime()
        => _time >= _timeToExplode;

    public void TriggerExplosion()
        => _isExplode = true;
}