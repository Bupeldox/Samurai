using Godot;
using System;

[Tool]
public partial class Limb : Node2D
{
	[Export]
	float length1;
	[Export]
	float length2;

	[Export]
	bool isKneeReversed;

	[Export]
	Node2D toNode,elbowNode,endNode,shoulderMinNode,startNode,rootNode;
	[Export]
	Node2D exportArmPos;

	[ExportGroup("Limits")]
	[Export]
	Node2D Limit1,Limit2;
	//if it doesn't work, swap the positions of these nodes.

	bool invertLimit = true;//only works if this is true
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	private float cosineRuleForAngle(float a, float b, float c){

		return Mathf.Acos((a*a+b*b-c*c)/(2*a*b));
		
	}
	private Vector2 getShoulderPos(Vector2 start,Vector2 target,Vector2 shoulderMin){
		var delta = target-start;
		var shoulderThreshold = length1/2;
		
		var stoStartLerp = start+((delta*0.1f).Normalized()*(Math.Min((delta*0.1f).Length(),shoulderThreshold)));
		//var stoStartLerp = start.add(target.sub(start).normalised(-0.2).rotate(Math.PI/4));
		//var stoSMinLerp = shoulderMin.add(target.sub(shoulderMin).times(0.1));
		//drawer.draw([shoulderMin,stoSMinLerp])
		var lerpAmount = 1-(Mathf.Min(shoulderThreshold,(start-target).Length())/shoulderThreshold);
		return stoStartLerp.Lerp(shoulderMin,lerpAmount);
	}



	private void updateJointPositions(){

		if(toNode==null||elbowNode==null||endNode==null||shoulderMinNode==null||startNode==null){
			return;
		}
		
		var fromNode = rootNode;
		var target = toNode.Position;

		var start = getShoulderPos(fromNode.Position,target,shoulderMinNode.Position);
		startNode.Position = start;
		var targetDelta = target-start;


		Vector2 elbowPos = Vector2.Zero;

		if(length1+length2<targetDelta.Length()){
			elbowPos = targetDelta.Normalized()*length1+start;
			//out of range so straight arm
		}
		else{
			
			var minLength = Math.Abs(length1-length2);

			if(minLength>targetDelta.Length()){
				//too close so move the target.
				targetDelta = targetDelta.Normalized()*(minLength+0.00001f);
			}
			
			var angleFromDeltaToElbow = cosineRuleForAngle(length1,targetDelta.Length(),length2)*(isKneeReversed?-1:1);
			
			var arm1Vec = targetDelta.Rotated(angleFromDeltaToElbow).Normalized()*length1;

			elbowPos = start+arm1Vec;
				
			
		}

		var relElbow =elbowPos-start;
		if(Limit1!=null && Limit2!=null){
			//first arm segment limits
			var minAnglePos = Limit1.Position;
			var maxAnglePos = Limit2.Position;
		
			

			
			//outside of bounds
			//xor is like invert if one of them is true
			if(invertLimit ^ (relElbow.AngleTo(minAnglePos)<0 && relElbow.AngleTo(maxAnglePos)>0)){
				//find closest
				if(Math.Abs(relElbow.AngleTo(minAnglePos))>Math.Abs(relElbow.AngleTo(maxAnglePos))){
					relElbow = maxAnglePos.Normalized()*length1;
				}else{
					relElbow = minAnglePos.Normalized()*length1;
				}
				elbowPos = relElbow+start;
			}
		}

		var elbowToTarget = target-elbowPos;

		if(Limit1!=null && Limit2!=null){
			//second arm segment limits
			float maxAngle = 2f*(3.1416f)/3f;
			var elbowAngle = relElbow.AngleTo(elbowToTarget);
			if(Math.Abs(elbowAngle)>maxAngle){
				
				elbowToTarget = relElbow.Rotated((isKneeReversed?1:-1)*maxAngle);//its normalised and stuff afterwards.
			}
			if(isKneeReversed^elbowAngle>0){
				elbowToTarget = relElbow;//its normalised and stuff afterwards.
			}

		}
		var endPos = (elbowToTarget.Normalized()*length2)+elbowPos;







		if(exportArmPos!=null){
			exportArmPos.Position = endPos;
		}
		endNode.Position = endPos;
		elbowNode.Position = elbowPos;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		updateJointPositions();
	}
}
