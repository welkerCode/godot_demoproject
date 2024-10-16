using Godot;
using System;

public partial class State : Node
{

	[Export] private String name = "state";

	public State(String new_name)
	{
		// All states should at least be instantiated with their name
		name = new_name;
	}

    public override void _Ready()
    {
		// Upon State creation, call the enter state function
        enter_state();
    }

    private void enter_state()
	{
		// This will contain the actions that should occur upon entering the state
		GD.Print("Entering State: " + name);
	}

	private void exit_state()
	{
		// This will contain the actions that should occur upon exiting the state
		GD.Print("Leaving State: " + name);
	}

	public State check_state_change(Node entity)
	{
		// This is the function that checks to see if we should go to a new state
		// If so, then create the new state, call the exit state function, and return the new state
		// Otherwise, return the current state

		// This is a junk if statement, as we will never really use the base class's version of this function, and it should be overwritten
		if (entity.Name == "Node")
		{
			State newState = new State("state");
			exit_state();
			return newState;
		}
		else
		{
			return this;
		}
	}
}
