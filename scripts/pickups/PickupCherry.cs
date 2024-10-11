using Godot;
using System;

public partial class PickupCherry : Pickup
{
	// Constructor to create a Cherry pickup
	public PickupCherry() : base("Cherry", 3)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
