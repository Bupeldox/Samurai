using Godot;
using System;

public partial class main : Node2D
{
	[Export]
	PackedScene[] Levels;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		var ins = Levels[0].Instantiate();
		AddChild(ins);
	}

    public override void _Input(InputEvent @event)
    {
	

        base._Input(@event);
    }
    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
	{
	}
}
