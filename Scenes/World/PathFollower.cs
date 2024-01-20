using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

public partial class PathFollower : PathFollow2D
{
	Path2D path;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		path = GetNode<Path2D>("Path2D");
	}

	[Export]
	float speed = 1;
	double inc = 0;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
		inc+=delta*speed;
		this.Progress = (float)inc;
	}
}
