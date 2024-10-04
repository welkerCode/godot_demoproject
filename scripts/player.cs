using Godot;
using System;

public partial class player : CharacterBody2D
{
	// Movement variables
	[Export] private float speed = 200f;  // Player speed
	[Export] private float jumpForce = 300f;  // Jump force
	[Export] private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();  // Gravity force
	[Export] private bool double_jump_ready = true;


	//private Vector2 velocity = new Vector2(x:Single = 0, y:Single = 0);  // Player velocity


	public override void _Ready()
	{
		bool double_jump_ready = true;
	}


	public override void _PhysicsProcess(double delta)
	{
		// Reset horizontal movement
		Vector2 velocity = Velocity;

		velocity.X = 0;

		// Get input for movement
		if (Input.IsActionPressed("ui_right"))
		{
			velocity.X += speed;  // Move right
			GD.Print("moving right");
		}
		if (Input.IsKeyPressed(Key.Left))
		{
			velocity.X -= speed;  // Move left
			GD.Print("moving left");
		}

		// Apply gravity
		velocity.Y += gravity * (float)delta;

		if (IsOnFloor())
		{
			double_jump_ready = true;
		}

		// Jumping
		if (IsOnFloor() && Input.IsActionJustPressed("ui_up"))
		{
			velocity.Y = -jumpForce;  // Jump
			GD.Print("Moving up");
		}
		if (!IsOnFloor() && double_jump_ready && Input.IsActionJustPressed("ui_up"))
		{
			velocity.Y = -jumpForce; // Doublejump
			GD.Print("Double jump");
			double_jump_ready = false;
		}

		GD.Print("Cycle.....");

		Velocity = velocity;

		// Move the player
		MoveAndSlide();
	}
}
