using Godot;
using System;

public partial class Spin : Sprite2D
{
	[Export]
	float speed=0.1f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		this.Rotate(speed);
	}
}
