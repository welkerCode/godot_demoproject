using Godot;
using System;

public partial class PickupCherry : Pickup
{

	[Export] private int Value = 3;
	[Export] private string Pickup_Name = "Cherry";

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
