namespace UU
{
    public interface IState
    {
        void Enter();
        void Exit();
        void HandleInput();
        void Update();
        void LateUpdate();
    }
}