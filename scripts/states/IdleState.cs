using Godot;
using System;

public partial class IdleState : State
{

	private AnimatedSprite2D animatedSprite2D;


	public IdleState(AnimatedSprite2D AnimatedSprite2D)
	{
		this.animatedSprite2D = AnimatedSprite2D;
	}

    public override void enter_state()
	{
		// This will contain the actions that should occur upon entering the state
		GD.Print("Entering Idle State");
		animatedSprite2D.Play("idle");
	}

	public override void exit_state()
	{
		// This will contain the actions that should occur upon exiting the state
		GD.Print("Leaving Idle State");
	}

	public override State check_state_change(Node entity)
	{
		// Check to see if we are the player
		if (entity is Player){

			// Cast the entity as a player
			Player p = entity as Player;

			// First, update the direction of the sprite if needed
			Vector2 new_direction = this.updateSpriteDirection();
			if(new_direction != new Vector2(0,0)){
				animatedSprite2D.Scale = new_direction;
			}

			// If we are jumping
			if (p.IsJumping()){
				p.playJumpSound();
				return new JumpingState(animatedSprite2D); // Return the running state
			}

			// If we are falling
			else if (p.Velocity.Y > 0){
				return new FallingState(animatedSprite2D); // Return the running state
			}
			
			// If we are running
			else if (p.IsRunning()){
				return new RunningState(animatedSprite2D); // Return the running state
			}

		}
		
		// Otherwise we will stay in this state
		return this;
	}
}
