using Godot;
using System;

public partial class WallSlideState : State
{

	private AnimatedSprite2D animatedSprite2D;


	public WallSlideState(AnimatedSprite2D animatedSprite2D)
	{
		this.animatedSprite2D = animatedSprite2D;
	}

    public override void enter_state()
	{
		// This will contain the actions that should occur upon entering the state
		GD.Print("Entering WallSlide State");
		animatedSprite2D.Play("wall slide");
	}

	public override void exit_state()
	{
		// This will contain the actions that should occur upon exiting the state
		GD.Print("Leaving WallSlide State");
	}

	public override State check_state_change(Node entity)
	{
		// Check to see if we are the player
		if (entity is Player){

			// Cast the entity as a player
			Player p = entity as Player;

			// If we jump from this state, then immediately enter jumping state

			// If we are still on the wall
			if (p.IsOnWall()){

				// Stop moving if wall sliding (for now)
				// TODO: reduce velocity, not stop it)
				Vector2 velocity = p.Velocity;
				float oldVelocity = velocity.Y;
				velocity.Y = 0;

				// If we have clicked the 'jump' button
				if (p.IsJumping()){

					// Change velocity to direction opposite of wall
					if (Input.IsActionPressed("move_left")){
						velocity.X = -200;
					}
					else {
						velocity.X = 200;
					}

					// Restore original y velocity, because jumping
					velocity.Y = oldVelocity;
					p.Velocity = velocity;

					// Reset double_jump_ready
					p.ResetDoubleJump();

					// change animation
					return new JumpingState(animatedSprite2D); //
				}

			}

			// Otherwise, if we are no longer on the wall, act as if we are falling
			else{
				// If we are double jumping
				if(p.IsDoubleJumping())
				{
					return new DoubleJumpingState(animatedSprite2D);
				}

				// If we are falling down and not touching a wall
				else if(p.Velocity.Y > 0){
					return new FallingState(animatedSprite2D);
				}
				

			}

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
