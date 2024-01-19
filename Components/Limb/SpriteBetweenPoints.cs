using Godot;
using System;

[Tool]
public partial class SpriteBetweenPoints : StaticBody2D
{
	[Export]
	Node2D nodeFrom;
	[Export]
	Node2D nodeTo;
	
	[Export]
	float rescale = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//ogWidth = GetNode<CollisionShape2D>("CollisionShape2D").Shape.GetRect().Size.X;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(nodeFrom==null || nodeTo==null){return;}
		this.Position = (nodeFrom.Position + nodeTo.Position)/2;
		
		//assume the width is 1;
		var deltaP = nodeFrom.Position - nodeTo.Position;

		var scale = (deltaP).Length()*rescale;

		this.Scale = new Vector2(scale,scale); 

		this.Rotation = -deltaP.AngleTo(Vector2.Right);

		//this.ConstantLinearVelocity = deltaP;


	}
}
