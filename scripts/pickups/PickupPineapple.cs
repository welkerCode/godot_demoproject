using Godot;
using System;

public partial class PickupPineapple : Pickup
{
	// Constructor to create a Pineapple pickup
	public PickupPineapple() : base("Pineapple", 7)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
