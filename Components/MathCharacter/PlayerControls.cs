using Godot;
using System;

public partial class PlayerControls : Node2D
{
	

	[Export]
	string action;
	[Export]
	float speed = 0.01f;
	[Export]
	Node2D armEnd;
	
	float joystickSensitivity = 100;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}


	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion eventMouseMotion){
			
			if(Input.IsActionPressed(action)){
				this.Position+=eventMouseMotion.Relative;
			}
		}
	}
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if(Input.IsActionJustPressed(action)){
			this.Position = armEnd.Position;
		}

			var velocity = Input.GetVector("move_left", "move_right", "move_up", "move_down");
			velocity = velocity*velocity.Abs();
			
			if(Input.IsActionPressed(action)){
				this.Position += velocity*joystickSensitivity;	
			}
		


	}
}
