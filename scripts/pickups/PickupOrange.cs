using Godot;
using System;

public partial class PickupOrange : Pickup
{
	// Constructor to create a Orange pickup
	public PickupOrange() : base("Orange", 6)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
