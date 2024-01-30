using Godot;
using System;

public partial class Cannon : Node2D, IAttackable
{
	[Export]
	PackedScene ammo;
	Node2D aim;
	[Export]
	float forceMult = 1;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		aim = GetNode<Node2D>("Aim");
	}

	public void ATTACK(){
		try{
			var  n = ammo.Instantiate();
			AddChild(n);
			RigidBody2D rb = (RigidBody2D)n;
			rb.ApplyCentralForce(aim.Position*forceMult);
		}
		catch(Exception ex){

		}

	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
