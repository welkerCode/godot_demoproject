using Godot;
using System;

public partial class Pickup : Area2D
{

	[Export] private int Value = 1;
	[Export] private string Pickup_Name = "Pickup";

	[Export] public AudioStreamPlayer SoundPlayer;
	[Export] public AudioStream PickupSound;
	[Export] public string PickupSoundPath = "res://audio/effects/item_pickup.wav";

    public override void _Ready()
    {
        GD.Print("Pickup " + Pickup_Name + " created with value: " + Value);

		GD.Print("Node Tree: " + this.GetParent().GetChildren().ToString());
		SoundPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");
	
		// Make sure the SoundPlayer is assigned in the editor
		if (SoundPlayer == null){

			GD.PrintErr("Error: AudioStreamPlayer node not found in Pickup.");
			// Create and add an AudioStreamPlayer node if it's not assigned via the editor
			//SoundPlayer = new AudioStreamPlayer();
			//AddChild(SoundPlayer);
		}

		// Optionally set a default sound if not already set in the editor
		if (PickupSound == null)
		{
			PickupSound = (AudioStream)GD.Load(PickupSoundPath);  // Default sound 
		}

		SoundPlayer.Stream = PickupSound;

    }

	private void playPickupSound(){
		//if (SoundPlayer.Playing){
		//	SoundPlayer.Stop();
		//}
		SoundPlayer.Play();
	}


    private void _on_body_entered(Node body)
	{
		GD.Print("Body: " + body + " has entered a pickup");
		GD.Print("Body Class: " + body.GetClass());
		if (body is CharacterBody2D)
		{
			GD.Print("A CharacterBody2D has entered the pickup...");
			if (body is Player)
			{
				GD.Print("The Player has entered...time to give points...");
				Player p = body as Player;
				p.update_score(Value);
			}
		}

		playPickupSound();

		QueueFree();
	}
}
