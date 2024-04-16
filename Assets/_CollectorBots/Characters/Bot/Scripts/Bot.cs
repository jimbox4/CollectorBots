using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bot : Character
{
    [SerializeField] private BotMover _mover;
    [Space]
    [SerializeField] private BotInteractSystem _interact;
    [SerializeField] private Transform _hand;
    [Space]
    [SerializeField] private BotAnimator _animator;
    [SerializeField] private BotAnimatorEvents _animatorEvents;

    private Crystal _crystal;
    private Vector3 _basePosition;
    private bool _isHandsEmpty = true;

    public bool HasCrystalRef => _crystal != null;

    private void Awake()
    {
        _mover.Initialize(GetComponent<Rigidbody>(), transform);
    }

    public void Initialize(Vector3 basePosition)
    {
        _basePosition = basePosition;
    }

    private void OnEnable()
    {
        _animatorEvents.TakeFrame += TakeCrystal;
    }

    private void OnDisable()
    {
        _animatorEvents.TakeFrame -= TakeCrystal;
    }

    private void Update()
    {
        UpdateAnimatorParams();

        if (_crystal == null && _isHandsEmpty)
        {
            _animator.SetIsRelax(true);
            _mover.ResetVelocitys();
            return;
        }

        if (_isHandsEmpty)
        {
            var crystalPosition = _crystal.Position;
            _mover.LookAt(crystalPosition);
            _mover.MoveTo(crystalPosition, _isHandsEmpty == false);
        }
        else
        {
            _mover.LookAt(_basePosition);
            _mover.MoveTo(_basePosition, _isHandsEmpty == false);
        }

        if (_crystal != null && _isHandsEmpty == true && GetDistaceTo(_crystal.Position) + 0.01f <= _mover.MaxDistanceToTarget)
        {
            _animator.SetPickUpState();
        }
    }

    public void SetCrystalRef(Crystal crystal)
    {
        _crystal = crystal;
        _animator.SetIsRelax(false);
    }

    public bool TryCollectCrystal(out Crystal crystal)
    {
        crystal = null;

        if (_crystal != null && _isHandsEmpty == false)
        {
            crystal = _crystal;
            _crystal.transform.SetParent(null);
            _isHandsEmpty = true;
            _crystal = null;
            _animator.ResetPickUpState();

            return true;
        }

        return false;
    }

    private void UpdateAnimatorParams()
    {
        _animator.SetHasItem(!_isHandsEmpty);
        _animator.SetSpeed(_mover.InterpolatedSpeed);
    }

    private void TakeCrystal()
    {
        if(_interact.TryGetCrystal(out _crystal))
        {
            _crystal.SetParent(_hand);
            _isHandsEmpty = false;
        }
    }

    private float GetDistaceTo(Vector3 to)
    {
        return Vector2.Distance(new Vector2(transform.position.x, transform.position.z), 
            new Vector2(to.x, to.z));
    }

    private void OnDrawGizmos()
    {
        _interact.DrawGizmos();
    }
}
