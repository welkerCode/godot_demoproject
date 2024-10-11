using Godot;
using System;

public partial class PickupMelon : Pickup
{
	// Constructor to create a Melon pickup
	public PickupMelon() : base("Melon", 5)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
