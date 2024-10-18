using Godot;
using System;

public partial class StateMachine : Node
{
    private State currentState;

    public void ChangeState(State newState)
    {

        // If the current state isn't 'null', then call the exit state function
        if (currentState != null)
        {
            currentState.exit_state();
        }

        // Then, call the new state's enter_state function
        currentState = newState;
        currentState.enter_state();
    }

    public void Update(Node entity)
    {
        // Use the entity (player, enemy, etc...) to check the current state
        State newState = currentState.check_state_change(entity);

        // If we got a different state than what we are now, then change state
        if (newState != currentState)
        {
            ChangeState(newState);
        }
    }
}
