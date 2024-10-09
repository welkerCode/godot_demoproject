using Godot;
using System;
using System.Diagnostics;

public partial class DangerZone : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Danger Zone ready");
		Connect("body_entered", new Callable(this, nameof(_on_area_2d_body_entered)));
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	//public override void _Process(double delta)
	//{
	//}
	
	private void _on_area_2d_body_entered(Node body)
	{
		GD.Print("Body: " + body + " has entered...");
		GD.Print("Body Class: " + body.GetClass());
		if (body is CharacterBody2D)
		{
			GD.Print("A CharacterBody2D has entered...");
			if (body is Player){
				GD.Print("The Player has entered...time to kill them...");
				Player p = body as Player;
				p.reset_player();
			}
		}
	}

}
