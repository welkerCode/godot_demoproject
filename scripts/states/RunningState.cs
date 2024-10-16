using Godot;
using System;

public partial class RunningState : State
{

	private AnimatedSprite2D animatedSprite2D;


	public RunningState(AnimatedSprite2D animatedSprite2D)
	{
		this.animatedSprite2D = animatedSprite2D;
	}

    public override void enter_state()
	{
		// This will contain the actions that should occur upon entering the state
		GD.Print("Entering Running State");
		animatedSprite2D.Play("running");
	}

	public override void exit_state()
	{
		// This will contain the actions that should occur upon exiting the state
		GD.Print("Leaving Running State");
	}

	public override State check_state_change(Node entity)
	{
		// Check to see if we are the player
		if (entity is Player){

			// Cast the entity as a player
			Player p = entity as Player;

			// If we are jumping
			if (p.IsJumping()){
				return new JumpingState(animatedSprite2D); // Return the running state
			}

			// If we are falling
			else if (p.Velocity.Y > 0){
				return new FallingState(animatedSprite2D); // Return the running state
			}
			
			// If we are idling
			else if(p.IsIdle()){
				return new IdleState(animatedSprite2D); // Return the running state
			}

		}
		
		// Otherwise we will stay in this state
		return this;
	}
}
