using Godot;
using System.Collections.Generic;

public partial class GameManager : Node
{
    [Export] private PackedScene[] levels; // Array of level scenes
    private Node2D levelRoot; // Reference to the LevelRoot node
    private int currentLevelIndex = 0;

    public override void _Ready()
    {
        levelRoot = GetNode<Node2D>("../LevelRoot"); // Adjust path to your LevelRoot
        LoadLevel(currentLevelIndex);
    }

	public void ClearLevel(){
		foreach (Node child in levelRoot.GetChildren()){
			child.QueueFree();
		}
	}

    public void LoadLevel(int levelIndex)
    {
        // Queue the existing level to be cleared
        ClearLevel();

        if (levelIndex >= 0 && levelIndex < levels.Length)
        {
            currentLevelIndex = levelIndex;

            // Instance the new level
            var levelInstance = levels[levelIndex].Instantiate();
            levelRoot.AddChild(levelInstance);

            GD.Print($"Loaded level {levelIndex}");
        }
        else
        {
            GD.PrintErr("Invalid level index");
        }
    }

    public void SpawnPickup(PackedScene pickupScene, Vector2 position)
    {
        if (pickupScene == null) return;

        var pickup = pickupScene.Instantiate<Area2D>();
        levelRoot.AddChild(pickup);
        pickup.GlobalPosition = position;

        GD.Print($"Spawned pickup at {position}");
    }

    public void DebugLoadLevel(int levelIndex)
    {
        LoadLevel(levelIndex);
        GD.Print($"Debugging level {levelIndex}");
    }
}
