using Godot;
using System;

public partial class PickupOrange : Pickup
{
	[Export] private int Value = 6;
	[Export] private string Pickup_Name = "Orange";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
