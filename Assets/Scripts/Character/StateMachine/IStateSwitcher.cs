namespace UU
{
    public interface IStateSwitcher
    {
        void SwitchState<State>() where State : IState;
    }
}