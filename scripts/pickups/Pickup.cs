using Godot;
using System;

public partial class Pickup : Area2D
{

	[Export] private int Value = 1;
	[Export] private string Pickup_Name = "Pickup";

	private AudioStreamPlayer _audioPlayer;

	public AudioStream PickupSound;

	public Pickup(string name, int value)
	{
		Pickup_Name = name;
		Value  = value;
	}

    public override void _Ready()
    {
        GD.Print("Pickup " + Pickup_Name + " created with value: " + Value);
		_audioPlayer = GetNode<AudioStreamPlayer>("AudioStreamPlayer");

		if (_audioPlayer == null)
		{
			GD.PrintErr("No AudioStreamPlayer node found!");
		}

		if (PickupSound != null){
			_audioPlayer.Stream = PickupSound;
		}
    }

	private void playPickupSound(){
		if (_audioPlayer.Playing){
			_audioPlayer.Stop();
		}
		_audioPlayer.Play();
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
