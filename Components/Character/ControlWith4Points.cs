using Godot;
using System;
using System.Linq;

[Tool]
public partial class ControlWith4Points : Polygon2D
{

	[Export]
	Node2D[] points;
	[Export]
	int internalsStartIndex=0;

	[Export]
	bool stop = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(stop){return;}

		try{

			if(!(points!=null && points.Count()>=4 && points[3] != null)){return;}
			for(var i=0;i<4;i++){
				this.Polygon[i+internalsStartIndex] = new Vector2(points[i].Position.X,points[i].Position.Y);
			}
		}
		catch(Exception ex){
			stop = true;
		}

	}
}
