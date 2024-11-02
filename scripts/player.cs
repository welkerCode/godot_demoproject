using Godot;
using System;

public partial class Player : CharacterBody2D
{
	// Movement variables
	[Export] public float _run_speed = 200f;  // Player speed
	[Export] public float _jump_force = 300f;  // Jump force
	[Export] public float _dash_speed = 2000; // Dash speed
	[Export] private float _gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();  // Gravity force
	[Export] private int _originX = 0;
	[Export] private int _originY = 0;
	[Export] private int _score = 0;
	[Export] private string _direction = "right";
	[Export] private bool _double_jump_ready = true;
	[Export] private bool _dash_ready = true;
	[Export] private bool _can_control = true;
	
	public Vector2 _next_velocity;
	private StateMachine _state_machine;
	private State _state;
	private AnimatedSprite2D _animated_sprite;

	//private Vector2 velocity = new Vector2(x:Single = 0, y:Single = 0);  // Player velocity

	private Vector2 _velocity;

	public override void _Ready()
	{
		// Initialize physics variables
		_next_velocity.X = 0;
		_next_velocity.Y = 0;

		// Initialize state machine
		_direction = "right";
		_animated_sprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		_state_machine = new StateMachine();
		IdleState idleState = new IdleState(this._animated_sprite);
		_state_machine.ChangeState(idleState);

		// Initialize player_control variable		
		_double_jump_ready = true;
		_dash_ready = true;
		
		// Initialize score variables
		_score = 0;
		
		GD.Print("Player Ready");
	}


	public Vector2 updateSpriteDirection(){

		/*
		This function should be called by the state machine to change direction based on user input
		
		Input: N/A
		Output: Vector2
			-1 in the X field indicates moving left
			1 in the X field indicates moving right
			A vector of 0,0 will indicate that the joystick isn't being moved left or right
		
		*/
	
		if (Input.IsActionPressed("move_left")){
			return new Vector2(-1, 1);
		}
		else if (Input.IsActionPressed("move_right")){
			return new Vector2(1, 1);
		}

		return new Vector2(0, 0);
	}

	public override void _PhysicsProcess(double delta)
	{
		// Call the state machine
		_state_machine.Update(this);

		// Apply gravity and update velocity
		_next_velocity = Velocity;
		_next_velocity.Y += _gravity * (float)delta;
		Velocity = _next_velocity;

		// Move the player
		MoveAndSlide();
	}

	public string getWhichWallCollided(){
		for (int i = 0; i < GetSlideCollisionCount(); i++){
			var collision = GetSlideCollision(i);
			if (collision.GetNormal().X > 0) return "left";
			else if (collision.GetNormal().X < 0) return "right";
		}
		return "none";
	}

	public Vector2 getPlayerInputForVelocity(){
		Vector2 new_velocity = new Vector2();
		new_velocity.X = getHorizontalInput();
		new_velocity.Y = getVerticalInput();
		return new_velocity;
	}

	public float getHorizontalInput(){

		float horizontalVelocity = 0;
		bool add_dash = _dash_ready && Input.IsActionJustPressed("ui_dash");

		if (Input.IsActionPressed("move_right"))
		{
			if (add_dash) horizontalVelocity += _dash_speed;
			horizontalVelocity += _run_speed;
		}
		else if (Input.IsActionPressed("move_left"))
		{
			if (add_dash) horizontalVelocity -= _dash_speed;
			horizontalVelocity -= _run_speed;
		}
		else
		{
			horizontalVelocity = 0;
		}
		return horizontalVelocity;
	}

	public float getVerticalInput(){
		float verticalVelocity = this.Velocity.Y;

		// Reset double jump if on ground
		if (IsOnFloor()) _double_jump_ready = true; 
		
		// Add jump force if jumping
		if (IsOnFloor() && Input.IsActionJustPressed("move_jump"))
		{
			verticalVelocity = -_jump_force;  // Jump
		}

		// Add jump force if double jumping
		if (!IsOnFloor() && _double_jump_ready && Input.IsActionJustPressed("move_jump"))
		{
			verticalVelocity = -_jump_force; // Doublejump
			_double_jump_ready = false; // Disable doublejump after doublejumping
		}

		return verticalVelocity;
	}

	public void ResetDoubleJump(){
		_double_jump_ready = true;
	}

	public bool IsIdle()
	{
		/*
		A simple function that returns true if the player is on the floor and not moving
		*/


		// Return true if we are not moving left or right, and on the floor
		return !(Input.IsActionPressed("move_left")) && !(Input.IsActionPressed("move_right")) && IsOnFloor();
	}

	public bool IsRunning()
	{
		/*
		A simple function that returns true if the player is on the floor and moving
		*/


		// Return true if we are moving left or right, and on the floor
		return ((Input.IsActionPressed("move_left")) || (Input.IsActionPressed("move_right"))) && IsOnFloor();
	}

	public bool IsJumping()
	{
		/*
		A simple function that returns true if the player is on the floor and jumping
		*/


		// Return true if we are jumping
		return (Input.IsActionJustPressed("move_jump") && IsOnFloor());
	}

	public bool IsDoubleJumping()
	{
		/*
		A simple function that returns true if the player is double jumping
		*/


		// Return true if we are double jumping
		return (Input.IsActionJustPressed("move_jump") && !IsOnFloor() && _double_jump_ready);
	}

	public void update_score(int points)
	{
		_score += points;
		GD.Print("Score: " + _score);
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
		_can_control = true;
	}

	public void handleDanger(){
		//GD.print("EnteredDanger");
		var visible = false; // TODO:Implement visibility???
		
		_can_control = true;
		var tree = GetTree();
		tree.CreateTimer(1); // Timeout...wait for a second
		reset_player();
	}

	
}
