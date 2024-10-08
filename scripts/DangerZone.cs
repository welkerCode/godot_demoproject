using Godot;
using System;

public partial class DangerZone : Area2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//+= OnBodyEnteredSignal;
		//Callable myCallable = new Callable(this, nameof(OnBodyEntered));

		// Ensure the player node is referenced correctly.
        //var player = GetNode<Player>("../Player"); // Update the path accordingly
        //DangerZone.Connect("body_entered", myCallable);
	}

	private void OnBodyEnteredSignal()
	{
		//GD.Print("Dead!!!!!!!!!!!!!!!!!!!!!!!!!!!");
		//if (body is Player player)
		//{
		//	player.OnDeathAreaBodyEntered();
		//}
	}
}