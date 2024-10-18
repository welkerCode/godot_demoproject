using Godot;
using System;

public partial class JumpingState : State
{

	private AnimatedSprite2D animatedSprite2D;


	public JumpingState(AnimatedSprite2D animatedSprite2D)
	{
		this.animatedSprite2D = animatedSprite2D;
	}

    public override void enter_state()
	{
		// This will contain the actions that should occur upon entering the state
		GD.Print("Entering Jumping State");
		animatedSprite2D.Play("jump");
	}

	public override void exit_state()
	{
		// This will contain the actions that should occur upon exiting the state
		GD.Print("Leaving Jumping State");
	}

	public override State check_state_change(Node entity)
	{
		// Check to see if we are the player
		if (entity is Player){

			// Cast the entity as a player
			Player p = entity as Player;

			// If we are double jumping
			if(p.IsDoubleJumping())
			{
				return new DoubleJumpingState(animatedSprite2D);
			}
			// If we are falling down
			else if(p.Velocity.Y > 0){
				return new FallingState(animatedSprite2D);
			}
			// If we are running
			else if(p.IsRunning()){
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