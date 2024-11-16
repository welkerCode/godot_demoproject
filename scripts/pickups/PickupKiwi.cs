using Godot;
using System;

public partial class PickupKiwi : Pickup
{
	[Export] private int Value = 4;
	[Export] private string Pickup_Name = "Kiwi";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
