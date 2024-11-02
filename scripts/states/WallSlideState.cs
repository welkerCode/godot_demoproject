using Godot;
using System;
using System.Runtime.CompilerServices;

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

	public override void ComputeVelocity(Node entity)
	{
		// Check to see if we are the player
		if (entity is Player){

			// Cast the entity as a player
			Player p = entity as Player;		

			// If we are still on the wall
			if (p.IsOnWall()){

				// Stick to the wall (TODO: Not working???)
				Vector2 velocity = p.Velocity;
				velocity.Y = 0;

				string wall_collision_side = p.getWhichWallCollided();

				// If we have clicked the 'jump' button
				if (!p.IsOnFloor() && Input.IsActionJustPressed("move_jump")){

					GD.Print("Wall JUMPING!!!!");
					velocity.Y = -p._jump_force;

					//if (leftHit.Count > 0)
					if (wall_collision_side == "left")
					{
						GD.Print("Wall is on the left!");
						velocity.X = p._run_speed;
					}
					//else if (rightHit.Count > 0)
					else if (wall_collision_side == "right")
					{
						GD.Print("Wall is on the right!");
						velocity.X = -p._run_speed;
					}

					p.Velocity = velocity;

					// Reset double_jump_ready
					p.ResetDoubleJump();
				}
			}
		}
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

			// If we are on the floor, then we must be idle or running
			if (p.IsOnFloor()){

				// If we aren't on the wall, and on the floor, then we must be in the idle or running state
				if (p.IsIdle()) return new IdleState(animatedSprite2D);
				else if (p.IsRunning()) return new RunningState(animatedSprite2D);
			}

			// If we are not on the floor, and are no longer on the wall
			if (!p.IsOnWall()){

				// And we are not on the floor
				if (!p.IsOnFloor())
				{
					// Then we are either jumping, double jumping or falling
					if (p.IsJumping()) return new JumpingState(animatedSprite2D);
					else if (p.IsDoubleJumping()) return new DoubleJumpingState(animatedSprite2D);
					else if (p.Velocity.Y > 0) return new FallingState(animatedSprite2D);
				}				
			}
		}

		// If we aren't on the floor, and still on a wall, then we must be still sliding, so stay in this state
		return this;
	}
}
