using System.Collections.Generic;

namespace UU
{
    public class CharacterStateMachine
    {
        private List<IState> _states;
        private IState _currentState;

        public CharacterStateMachine()
        {
            _states = new List<IState>()
            {

            };

            _currentState = _states[0];
            _currentState.Enter();
        }
    } 
}