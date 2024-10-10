using Godot;
using System;

public partial class PickupBanana : Pickup
{
	// Constructor to create a banana pickup
	public PickupBanana() : base("Banana", 2)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
