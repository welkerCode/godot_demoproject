using Godot;
using System;

public partial class PickupStrawberry : Pickup
{
	[Export] private int Value = 8;
	[Export] private string Pickup_Name = "Strawberry";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
