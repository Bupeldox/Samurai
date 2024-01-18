using Godot;
using System;

public partial class player : Node2D
{

	private RigidBody2D LeftArm;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LeftArm = GetNode<RigidBody2D>("UpperArm/LowerArm");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Q"))
		{
			ApplyForceToLimb(LeftArm, 50);
		}
	}

	public void ApplyForceToLimb(RigidBody2D limb, float force)
	{
		// Get location of cursor
		Vector2 cursorPos = GetGlobalMousePosition();

		// Apply force to limb towards cursor
		limb.ApplyCentralImpulse((cursorPos - limb.GlobalPosition).Normalized() * force);
	}
}
