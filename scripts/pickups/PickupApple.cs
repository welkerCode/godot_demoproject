using Godot;
using System;

public partial class PickupApple : Pickup
{

	[Export] private int Value = 1;
	[Export] private string Pickup_Name = "Apple";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
