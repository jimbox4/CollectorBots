using UnityEngine;

public class BotCollectState : IBotState
{
    private readonly Bot _bot;
    private readonly BotMover _mover;

    private bool _isSlowed = false;
    private Vector3 _crystalPosition;

    public BotCollectState(Bot bot, BotMover mover)
    {
        _bot = bot ?? throw new System.ArgumentNullException(nameof(bot));
        _mover = mover ?? throw new System.ArgumentNullException(nameof(mover));
    }

    public void Enter()
    {
        _crystalPosition = _bot.CrystalPosition;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        _mover.LookAt(_crystalPosition);
        _mover.MoveTo(_crystalPosition, _isSlowed);
    }
}
