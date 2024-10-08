using Godot;
using System;
using System.Diagnostics;

public partial class Water : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Water ready");
		Connect("body_entered", new Callable(this, nameof(_on_area_2d_body_entered)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
	
	private void _on_area_2d_body_entered(Node body)
	{
		GD.Print("Body: " + body + " has entered...");
	}
}
