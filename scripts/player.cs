using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Movement variables
	[Export] private float speed = 200f;  // Player speed
	[Export] private float jumpForce = 300f;  // Jump force
	[Export] private float dashSpeed = 2000; // Dash speed
	[Export] private float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();  // Gravity force
	[Export] private string direction = "right";
	[Export] private bool double_jump_ready = true;
	[Export] private bool dash_ready = true;
	[Export] private bool can_control = true;
	[Export] private int origin_x = 0;
	[Export] private int origin_y = 0;
	[Export] private int score = 0;
	private string state = "Idle";

	private AnimatedSprite2D _animatedSprite;

	//private Vector2 velocity = new Vector2(x:Single = 0, y:Single = 0);  // Player velocity


	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		double_jump_ready = true;
		dash_ready = true;
		direction = "right";
		score = 0;
		GD.Print("Player Ready");

		//Area2D DangerZone_area = GetNode<Area2D>("../Area2D");
		//DangerZone_area.Connect("danger_zone_entered", this, nameof(OnDangerZoneEntered));
	}


	public override void _PhysicsProcess(double delta)
	{
		// If you can no longer control the character, then return, don't do anything
		//if (can_control != true){return;}

		// Reset horizontal movement
		Vector2 velocity = Velocity;

		velocity.X = 0;


		switch (state)
		{
			case "Idle":

				if ((Input.IsActionPressed("move_left")) || (Input.IsActionPressed("move_right")))
				{
					state = "Run";
					_animatedSprite.Play("run");
				}
				break;

			case "Run":

				if (!IsOnFloor())
				{
					state = "Fall";
					_animatedSprite.Play("fall");
				}
				
				if (Input.IsActionJustPressed("ui_up"))
				{
					state = "Jump";
					_animatedSprite.Play("jump");
				}
					
				else if (!(Input.IsActionPressed("move_left")) && !(Input.IsActionPressed("move_right")))
				{
					state = "Idle";
					_animatedSprite.Play("idle");
				}
				break;

			case "Jump":
				if (IsOnFloor())
				{
					if (!(Input.IsActionPressed("move_left")) && !(Input.IsActionPressed("move_right")))
					{
						state = "Idle";
						_animatedSprite.Play("idle");
					}
					else if ((Input.IsActionPressed("move_left")) || (Input.IsActionPressed("move_right")))
					{
						state = "Run";
						_animatedSprite.Play("run");
					}
				}
				else 
				{
					if (double_jump_ready && Input.IsActionJustPressed("ui_up")){
						state = "Double Jump";
						_animatedSprite.Play("double jump");
					}
					else if (velocity.Y > 0)
					{
						state = "Fall";
						_animatedSprite.Play("fall");
					}

				}
				break;

			case "Double Jump":
				if (IsOnFloor())
				{
					if (!(Input.IsActionPressed("move_left")) && !(Input.IsActionPressed("move_right")))
					{
						state = "Idle";
						_animatedSprite.Play("idle");
					}
					else if ((Input.IsActionPressed("move_left")) || (Input.IsActionPressed("move_right")))
					{
						state = "Run";
						_animatedSprite.Play("run");
					}
				}
				break;

			case "Wall Jump":
				break;

			case "Fall":
				if (IsOnFloor())
				{
					if (!(Input.IsActionPressed("move_left")) && !(Input.IsActionPressed("move_right")))
					{
						state = "Idle";
						_animatedSprite.Play("idle");
					}
					else if ((Input.IsActionPressed("move_left")) || (Input.IsActionPressed("move_right")))
					{
						state = "Run";
						_animatedSprite.Play("run");
					}
				}
				else {
					if (double_jump_ready && Input.IsActionJustPressed("ui_up"))
					{
						state = "Double Jump";
						_animatedSprite.Play("double jump");
					}
				}
				break;
			
			case "Hit":
				break;
		}

		// Get input for movement
		if (Input.IsActionPressed("move_right"))
		{
			velocity.X += speed;  // Move right
			direction = "right";
			//GD.print("moving right");
		}
		if (Input.IsActionPressed("move_left"))
		{
			velocity.X -= speed;  // Move left
			direction = "left";
			//GD.print("moving left");
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
			//GD.print("Moving up");
		}
		if (!IsOnFloor() && double_jump_ready && Input.IsActionJustPressed("ui_up"))
		{
			velocity.Y = -jumpForce; // Doublejump
			//GD.print("Double jump");
			double_jump_ready = false;
		}

		if (dash_ready && Input.IsActionJustPressed("ui_dash"))
		{
			 
			if (direction == "right")
			{
				velocity.X += dashSpeed;
				//dash_ready = false;
			}
			else if (direction == "left")
			{
				velocity.X -= dashSpeed;
				//dash_ready = false;
			}
		}

		Velocity = velocity;

		// Move the player
		MoveAndSlide();
	}

	public void update_score(int points)
	{
		score += points;
		GD.Print("Score: " + score);
	}

	public void OnDangerZoneEntered()
	{
		GD.Print("Dying...");
		CheckPlayerDeath();
	}

	public void CheckPlayerDeath(){
		var tree = GetTree();
		tree.ReloadCurrentScene();
	}

	public void reset_player()
	{
		// Reset the position
		Vector2 position = Position;
		position.X = 1;
		position.Y = -20;
		Position = position;
	
		// Reset the controls so we can play again
		can_control = true;
	}

	public void handleDanger(){
		//GD.print("EnteredDanger");
		var visible = false; // TODO:Implement visibility???
		
		can_control = true;
		var tree = GetTree();
		tree.CreateTimer(1); // Timeout...wait for a second
		reset_player();
	}

	
}
