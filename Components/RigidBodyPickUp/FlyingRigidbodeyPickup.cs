using Godot;
using System;
using System.Data;
using System.Linq;


public class TimeOutThing{

	private Func<bool> func;
	double timer = 0;
	float time;
	public TimeOutThing(float time,Func<bool> func)
	{
		this.time = time;
		this.func = func;
	}
	public void Update(double delta){
		timer+=delta;
		if(timer>time){
			func();
		}
	}
}

public partial class FlyingRigidbodeyPickup : RigidBody2D
{
	GpuParticles2D pop;
	Sprite2D sprite;

	TimeOutThing removeTimer;
	bool hasHit = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ContactMonitor = true;
		MaxContactsReported = 20;
		pop = GetNode<GpuParticles2D>("GPUParticles2D");
		pop.Emitting = false;
		sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public void onHit(){
		pop.Emitting = true;
		removeTimer = new TimeOutThing(5,()=>{QueueFree();return false;});
	}


	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		removeTimer?.Update(delta);

		if(hasHit){return;}

		if(this.Position.X > 1000){
			if(removeTimer==null){
				removeTimer = new TimeOutThing(5,()=>{QueueFree();return false;});
			}
			return;
		}

		var bods = GetCollidingBodies();
		if(bods.Count==0 || !bods.Any(i=>i.HasMeta("Damageable"))){
			return;
		}
		this.onHit();
	}
}
