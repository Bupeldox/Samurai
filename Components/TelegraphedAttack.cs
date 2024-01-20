using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;

public partial class TelegraphedAttack : Area2D
{
	[Export]
	float telegraphTime = 3f;


	bool isGoing = false;
	float attackHoldTime = 1f;
	Polygon2D polygon;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		polygon = GetNode<Polygon2D>("Polygon2D");
	}

	public void StartAttack(){
		isGoing = true;
		polygon.Color = new Color("#00448833");
	}

	float telegraphOnOffTime = 0;
	bool telegraphFlashOn = false;
	private void telegraphFlash(){
		float telegraphPWM = (telegraphTime-time)/2f;
		if(telegraphOnOffTime>telegraphPWM){
			telegraphOnOffTime = 0;
			telegraphFlashOn = !telegraphFlashOn;

			if(telegraphFlashOn){
				GD.Print("TelegraphHigh");
				polygon.Color = new Color("#0088ff88");

			}else{
				GD.Print("TelegraphOff");
				polygon.Color = new Color("#00448833");
			}
		}
	}
	float time = 0;
	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if(!isGoing){
			polygon.Visible = false;
			time = 0f; 
			return;
		}

		time+=(float)delta;

		//Telegraph
		if(time<telegraphTime){
			polygon.Visible = true;
			telegraphFlash();
			telegraphOnOffTime+=(float)delta;
			return;
		}

		//Attacking
		
		//turn off at end
		if(time>telegraphTime+attackHoldTime){
			isGoing = false;
			return;
		}
		
		//attack period
		if(time>telegraphTime){
			polygon.Color = new Color("#ff2222");
			return;
		}
		
		
		


	}
}
