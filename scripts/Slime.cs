using Godot;
using System;

public partial class Slime : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	private void _on_area_2d_body_entered(Node body)
	{
		GD.Print("Body: " + body + " has entered...");
		GD.Print("Body Class: " + body.GetClass());
		if (body is CharacterBody2D)
		{
			GD.Print("A CharacterBody2D has entered...");
			if (body is Player){
				GD.Print("The Player has entered...time to kill them...");
				Player p = body as Player;
				p.reset_player();
			}
		}
	}
}
