using Godot;
using System;

public abstract partial class State : Node
{

	[Export] private String name;

    public abstract void enter_state();

	public abstract void exit_state();

	public abstract State check_state_change(Node entity);
}
