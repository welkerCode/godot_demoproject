using Godot;
using System;

public partial class PickupBanana : Pickup
{

	[Export] private int Value = 2;
	[Export] private string Pickup_Name = "Banana";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
