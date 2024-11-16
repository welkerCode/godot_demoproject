using Godot;
using System;

public partial class PickupPineapple : Pickup
{
	[Export] private int Value = 7;
	[Export] private string Pickup_Name = "Pineapple";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
