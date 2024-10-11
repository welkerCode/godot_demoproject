using Godot;
using System;

public partial class PickupKiwi : Pickup
{
	// Constructor to create a Kiwi pickup
	public PickupKiwi() : base("Kiwi", 4)
	{
	}	

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
	}
}
