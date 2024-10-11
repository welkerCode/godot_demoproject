using Godot;
using System;

public partial class PickupApple : Pickup
{
	// Constructor to create a Apple pickup
	public PickupApple() : base("Apple", 1)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
