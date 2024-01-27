using Godot;
using System;

public partial class StaticBodyPhysics : StaticBody2D
{	
	Vector2 prevPos;
	float prevRotation;
	
	Vector2 prevVelocity=Vector2.Zero;
	float prevAngularVelocity = 0;

	float maxAcceleraion = 2f;
	float maxAngularAcceleration = 0.001f;

	float maxVelocity = 1000f;
	float maxAngularVelocity = 0.5f;

	float debugVel = 0;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		updatePrevs();
	}

	private void updatePrevs(){
		
		prevPos = GlobalPosition*1f;
		prevRotation = GlobalRotation;
	}

    public override void _PhysicsProcess(double delta)
    {
		var detlaP =  (GlobalPosition-prevPos)/(float)delta; 
		var deltaR =  (GlobalRotation-prevRotation)/(float)delta;

		if(debugVel<deltaR){
			debugVel = deltaR;
		}

		detlaP = detlaP.Normalized()* (Mathf.Min(detlaP.Length(),maxVelocity));
		deltaR = Mathf.Min(deltaR,maxAngularVelocity);

/*
		//mouse movement is a bit wierd.
		if(detlaP.Length()>prevVelocity.Length()){
			var jerk = detlaP-prevVelocity;
			jerk = jerk.Normalized()*Mathf.Min(detlaP.Length(),maxAcceleraion);
			detlaP = prevVelocity+jerk;
		}

		if(deltaR>prevAngularVelocity){
			var jerk = deltaR-prevAngularVelocity;
			jerk = Mathf.Min(deltaR,maxAngularAcceleration);
			deltaR = prevAngularVelocity+jerk;

		}
*/
		/*
		if(detlaP.Length()!=0){
			GD.Print("After");
			GD.Print(detlaP.Length());
			GD.Print(deltaR);
		}
		*/
		prevAngularVelocity = deltaR;
		prevVelocity = detlaP;

		ConstantLinearVelocity = detlaP;
		ConstantAngularVelocity = deltaR;

		updatePrevs();

        base._PhysicsProcess(delta);
    }
	
}
