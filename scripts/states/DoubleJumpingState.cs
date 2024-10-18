using Godot;
using System;

public partial class DoubleJumpingState : State
{

	private AnimatedSprite2D animatedSprite2D;


	public DoubleJumpingState(AnimatedSprite2D animatedSprite2D)
	{
		this.animatedSprite2D = animatedSprite2D;
	}

    public override void enter_state()
	{
		// This will contain the actions that should occur upon entering the state
		GD.Print("Entering DoubleJumping State");
		animatedSprite2D.Play("double jump");
	}

	public override void exit_state()
	{
		// This will contain the actions that should occur upon exiting the state
		GD.Print("Leaving DoubleJumping State");
	}

	public override State check_state_change(Node entity)
	{
		// Check to see if we are the player
		if (entity is Player){

			// Cast the entity as a player
			Player p = entity as Player;

			// If we are running
			if(p.IsRunning()){
				return new RunningState(animatedSprite2D); // Return the jumping state
			}
			// If we are idle
			else if(p.IsIdle()){
				return new IdleState(animatedSprite2D);
			}

		}
		
		// Otherwise we will stay in this state
		return this;
	}
}
