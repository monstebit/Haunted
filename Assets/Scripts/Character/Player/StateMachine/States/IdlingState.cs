namespace UU
{
    public class IdlingState : MovementState
    {
        public IdlingState(
            IStateSwitcher stateSwitcher, 
            PlayerInputManager playerInputManager, 
            StateMachineData data) : base(stateSwitcher, playerInputManager, data)
        {
        }

        public override void Update()
        {
            base.Update();
            
            if (IsMovementInputZero())
                return;
            
            StateSwitcher.SwitchState<WalkingState>();
        }
    }
}