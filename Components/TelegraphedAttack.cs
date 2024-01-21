using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class TelegraphedAttack : Area2D
{
	[Export]
	float telegraphTime = 3f;

	[Signal]
    public delegate void DoDamageEventHandler();


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
			hasHit = false;
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
			doAttack();
			return;
		}
		
		
		

	}

	bool hasHit = false;
	private void doAttack(){

			if(hasHit){
				//onlyHitOnce.
				return;
			}

			var bods = GetOverlappingBodies();

			var damageable = bods.FirstOrDefault(i=>
			i.HasMeta("Damageable") && i.GetMeta("Damageable").As<bool>(),null);
			if(damageable==null){
				return;
			}

			hasHit = true;
			EmitSignal(SignalName.DoDamage);

		}

}
