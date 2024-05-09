using System.Collections.Generic;
using System.Linq;

namespace UU
{
    public class CharacterStateMachine : IStateSwitcher
    {
        private List<IState> _states;
        private IState _currentState;

        public CharacterStateMachine(PlayerInputManager playerInputManager)
        {
            StateMachineData data = new StateMachineData();
            
            _states = new List<IState>()
            {
                new IdlingState(this, playerInputManager, data),
                new WalkingState(this, playerInputManager, data),
                new SprintingState(this, playerInputManager, data),
            };

            _currentState = _states[0];
            _currentState.Enter();
        }

        public void SwitchState<State>() where State : IState
        {
            //  НАШЛИ ТЕКУЩЕЕ СОСТОЯНИЕ
            IState state = _states.FirstOrDefault(state => state is State);
            //  ВЫХОДИМ ИЗ ТЕКУЩЕГО СОСТОЯНИЯ
            _currentState.Exit();
            //  ПРИСВАЕВАЕМ ТЕКУЩЕМУ СОСТОЯНИЮ НОВОЕ
            _currentState = state;
            //  ВХОДИМ В НОВОЕ СОСТОЯНИЕ
            _currentState.Enter();
        }

        public void HandleInput() => _currentState.HandleInput();
        
        public void Update() => _currentState.Update();
    } 
}