using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Bot : MonoBehaviour
{
    [SerializeField] private BotMover _mover;
    [Space]
    [SerializeField] private BotCollector _interact;
    [SerializeField] private Transform _hand;
    [Space]
    [SerializeField] private BotAnimator _animator;
    [SerializeField] private BotAnimatorEvents _animatorEvents;

    private IBotState _currentState;
    private Dictionary<BotStateType, IBotState> _botStates;

    private Crystal _crystal;
    private bool _isHandsEmpty = true;

    public Vector3 BasePosition { get; private set; }

    public bool HasCrystalRef => _crystal != null;
    public Vector3 CrystalPosition => _crystal.Position;

    private void Awake()
    {
        _mover.Initialize(GetComponent<Rigidbody>(), transform);

        _botStates = new Dictionary<BotStateType, IBotState>()
        {
            {BotStateType.Return, new BotReturnState(this, _mover) },
            {BotStateType.Collect, new BotCollectState(this, _mover) },
            {BotStateType.Wait, new BotWaitState(_mover) },
        };

        _currentState = _botStates[BotStateType.Wait];
    }

    public void Initialize(Vector3 basePosition)
    {
        BasePosition = basePosition;
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
        _animator.SetSpeed(_mover.Speed);

        _currentState.Update();

        if (_crystal == null && _isHandsEmpty == false)
        {
            return;
        }

        if (IsReachedDistance())
        {
            _animator.SetPickUpState();
        }
    }

    public void SetCrystalRef(Crystal crystal)
    {
        _crystal = crystal;
        _animator.SetIsRelax(false);
        ChangeState(BotStateType.Collect);
    }

    public bool TryCollectCrystal(out Crystal crystal)
    {
        crystal = null;

        if (_isHandsEmpty == false)
        {
            ChangeState(BotStateType.Wait);
            crystal = _crystal;
            _crystal.transform.SetParent(null);
            _isHandsEmpty = true;
            _crystal = null;
            _animator.SetHasItem(_isHandsEmpty == false);
            _animator.ResetPickUpState();

            return true;
        }

        return false;
    }

    private void ChangeState(BotStateType stateType)
    {
        if (_botStates.ContainsKey(stateType) == false)
        {
            throw new System.ArgumentException(nameof(stateType));
        }

        if (_botStates[stateType] == _currentState)
        {
            return;
        }

        _currentState.Exit();
        _currentState = _botStates[stateType];
        _currentState.Enter();
    }

    private bool IsReachedDistance() 
        => GetDistaceTo(_crystal.Position) + 0.01f <= _mover.MaxDistanceToTarget;

    private void TakeCrystal()
    {
        if (_interact.TryGetCrystal(out _crystal))
        {
            _crystal.SetParent(_hand);
            _isHandsEmpty = false;
            ChangeState(BotStateType.Return);
            _animator.SetHasItem(_isHandsEmpty == false);
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
