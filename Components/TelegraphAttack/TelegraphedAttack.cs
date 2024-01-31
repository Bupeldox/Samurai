using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

public partial class TelegraphedAttack : Area2D, IAttackable
{
	[Export]
	float telegraphTime = 3f;

	[Signal]
    public delegate void DoDamageEventHandler();


	bool isGoing = false;
	float attackHoldTime = 1f;
	Polygon2D polygon;
	GpuParticles2D particles;
	CollisionPolygon2D collisionPolygon;

	//List<RayCast2D> rays;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		polygon = GetNode<Polygon2D>("Polygon2D");
		particles = GetNode<GpuParticles2D>("GPUParticles2D");
		collisionPolygon = GetNode<CollisionPolygon2D>("CollisionPolygon2D");

		var thing = collisionPolygon.Polygon;

		// for(var i=1;i<thing.Length;i++){
		// 	var to = thing[i-1];
		// 	var from = thing[i];

		// 	var ray = new RayCast2D();
		// 	this.AddChild(ray);
		// 	ray.Position = from;
		// 	ray.TargetPosition = to;
		// 	rays.Add(ray);
		// }

	}

	public void ATTACK(){
		isGoing = true;
		polygon.Color = new Color("#f5212233");
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
				polygon.Color = new Color("#f5212293");

			} else {
				GD.Print("TelegraphOff");
				polygon.Color = new Color("#f5212233");
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
	

	// private List<Node2D> getOverlappingBodiesWithRays(){
	// 	rays.
	// }

	bool hasHit = false;
	private void doAttack(){
		
			if(hasHit){
				//onlyHitOnce.
				return;
			}

			var bods = GetOverlappingBodies();
			//getOverlappingBodiesWithRays();//;

			var damageable = bods.FirstOrDefault(i=>
				i.HasMeta("Damageable") && 
				i.GetMeta("Damageable").As<bool>()
			,null);
			
			if(damageable==null){
				return;
			}

			hasHit = true;
			EmitSignal(SignalName.DoDamage);
			particles.Emitting = true;

		}

}
