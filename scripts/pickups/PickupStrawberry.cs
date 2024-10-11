using Godot;
using System;

public partial class PickupStrawberry : Pickup
{
	// Constructor to create a Strawberry pickup
	public PickupStrawberry() : base("Strawberry", 8)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
