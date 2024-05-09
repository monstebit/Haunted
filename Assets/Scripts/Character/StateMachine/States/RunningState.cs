namespace UU
{
    public class RunningState : MovementState
    {
        public RunningState(IStateSwitcher stateSwitcher, PlayerInputManager playerInputManager, StateMachineData data) : base(stateSwitcher, playerInputManager, data)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Data.RunningSpeed = 5f;
        }

        public override void Update()
        {
            base.Update();
            
            if (IsHorizontalInputZero() || IsVerticalInputZero())
                StateSwitcher.SwitchState<IdlingState>();
        }
    }
}