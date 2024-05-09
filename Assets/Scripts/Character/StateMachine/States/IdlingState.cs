namespace UU
{
    public class IdlingState : MovementState
    {
        public IdlingState(IStateSwitcher stateSwitcher, PlayerInputManager playerInputManager, StateMachineData data) : base(stateSwitcher, playerInputManager, data)
        {
        }

        public override void Update()
        {
            base.Update();
            
            if (IsHorizontalInputZero() || IsVerticalInputZero())
                return;
            
            StateSwitcher.SwitchState<RunningState>();
        }
    }
}