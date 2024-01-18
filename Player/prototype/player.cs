using Godot;
using System;

public partial class player : Node2D
{

	private RigidBody2D LeftArm;
	private RigidBody2D RightArm;
	private RigidBody2D LeftLeg;
	private RigidBody2D RightLeg;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LeftArm = GetNode<RigidBody2D>("UpperArmLeft/LowerArmLeft");
		RightArm = GetNode<RigidBody2D>("UpperArmRight/LowerArmRight");
		LeftLeg = GetNode<RigidBody2D>("UpperLegLeft/LowerLegLeft");
		RightLeg = GetNode<RigidBody2D>("UpperLegRight/LowerLegRight");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionPressed("Q"))
		{
			ApplyForceToLimb(LeftArm, 50);
		}
		else if(Input.IsActionPressed("E"))
		{
			ApplyForceToLimb(RightArm, 50);
		}
		else if(Input.IsActionPressed("W"))
		{
			ApplyForceToLimb(LeftLeg, 50);
		}
		else if(Input.IsActionPressed("R"))
		{
			ApplyForceToLimb(RightLeg, 50);
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
