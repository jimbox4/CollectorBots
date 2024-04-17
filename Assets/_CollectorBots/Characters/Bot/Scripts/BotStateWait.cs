public class BotWaitState : IBotState
{
    private readonly BotMover _botMover;

    public BotWaitState(BotMover botMover)
    {
        _botMover = botMover;
    }

    public void Enter()
    {
        _botMover.ResetVelocitys();
    }

    public void Exit()
    {

    }

    public void Update()
    {

    }
}
