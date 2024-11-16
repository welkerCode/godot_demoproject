using Godot;
using System;

public partial class PickupMelon : Pickup
{
	[Export] private int Value = 5;
	[Export] private string Pickup_Name = "Melon";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
