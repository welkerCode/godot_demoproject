using Godot;
using System;

public abstract partial class State : Node
{

	[Export] private String name;

    public abstract void enter_state();

	public abstract void exit_state();

	public abstract State check_state_change(Node entity);

	public Vector2 updateSpriteDirection(){

		/*
		This function should be called by the state machine to change direction based on user input
		
		Input: N/A
		Output: Vector2
			-1 in the X field indicates moving left
			1 in the X field indicates moving right
		
		*/
	
		if (Input.IsActionPressed("move_left")){
			return new Vector2(-1, 1);
		}
		else if (Input.IsActionPressed("move_right")){
			return new Vector2(1, 1);
		}

		return new Vector2(0, 0);
	}
}
