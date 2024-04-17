using UnityEngine;

public class BotReturnState : IBotState
{
    private readonly Bot _bot;
    private readonly BotMover _mover;

    private Vector3 _basePosition;
    private bool _isSlowed = true;

    public BotReturnState(Bot bot, BotMover botMover)
    {
        _bot = bot ?? throw new System.ArgumentNullException(nameof(bot));
        _mover = botMover ?? throw new System.ArgumentNullException(nameof(botMover));
    }

    public void Enter()
    {
        _basePosition = _bot.BasePosition;
    }

    public void Exit()
    {

    }

    public void Update()
    {
        _mover.LookAt(_basePosition);
        _mover.MoveTo(_basePosition, _isSlowed);
    }
}
