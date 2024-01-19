using Godot;
using System;

public partial class RespawnIfFallen : RigidBody2D
{
	Vector2 startPos;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		startPos = this.Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(this.Position.Y>GetViewportRect().End.Y-100){
			//SetPhysicsProcess(false);
			//var t = GetTransform();
			//t.Origin-=(startPos-Position);
			//Transform = t ;
			//this.LinearVelocity = Vector2.Zero;
			//this.AngularVelocity = 0;
			this.ApplyImpulse((startPos-this.Position).Normalized()*100f);
			//SetPhysicsProcess(true);
		}
	}
}
