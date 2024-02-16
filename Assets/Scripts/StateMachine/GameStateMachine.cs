using UnityEngine;

namespace StateMachine
{
    public class GameStateMachine : SingletonInstance<GameStateMachine>
    {
        [SerializeField]
        private bool _enableDebugging = true;

        private StateMachine StateMachine { get; set; } = new();
        
        public State CurrentState => StateMachine.CurrentState;
        public int StateMachineHashCode => StateMachine.GetHashCode();

        public bool IsDebugging => _enableDebugging;

        private void Awake()
        {
            if (_enableDebugging)
                Debug.Log($"Initialising StateMachine ({StateMachineHashCode})...");

            // Example of how to set the initial state.
            SetState<BootState>();
        }

        private void Start() => StateMachineTestStart();
        
        private void Update() => StateMachineTestUpdate();

        public void Subscribe(State state) => StateMachine.Subscribe(state);

        /// <summary>
        /// Sets the current state to the state parsed in the param.
        /// </summary>
        /// <param name="newState">The object reference of the state to change to.</param>
        public void SetState(State newState) => StateMachine.SetState(newState);

        /// <summary>
        /// Sets the current state to the state parsed in the Type param.
        /// </summary>
        /// <typeparam name="TState">The type reference of the state to change to.</typeparam>
        public void SetState<TState>() where TState : State, new()
            => StateMachine.SetState<TState>();

        /// <summary>
        /// Retrieves the state of type <typeparamref name="TState"/> from the owning state machine.
        /// </summary>
        /// <typeparam name="TState">The type of the state to retrieve.</typeparam>
        /// <returns>The state of type <typeparamref name="TState"/>.</returns>
        public State GetState<TState>() where TState : State, new() => StateMachine.GetState<TState>();

        /// <summary>
        /// Moves the state machine to the next state in the static flow.
        /// </summary>
        public void MoveToNextState() => StateMachine.MoveToNextState();

#region Testing
        private void StateMachineTestStart() 
            => StateMachine.GetState<GameState>().OnStateEnter += DebugStateInstance;

        private void StateMachineTestUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                DebugNextState();

            if (Input.GetKeyDown(KeyCode.I))
                DebugPrintStates();
        }

        private void DebugNextState() => StateMachine.MoveToNextState();

        private void DebugPrintStates()
        {
            print("Currently Subscribed States: ");
            for (int i = 0; i < StateMachine.States.Count; i++)
                print($"{i} : {StateMachine.States[i]} : {StateMachine.States[i].GetHashCode()}");
        }

        private void DebugStateInstance()
        {
            if (_enableDebugging)
                print("I work!");
        }
#endregion
    }
}
